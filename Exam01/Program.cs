using System.Text.Json;
using BDRD.DemoCICD.CRUDAPP.Web.Extensions;
using Exam01.Application.Repository;
using Exam01.Application.Service;
using Exam01.BackgroundTasks;
using Exam01.Database;
using Exam01.Hubs;
using Exam01.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;
builder.Services.AddControllersWithViews()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddSignalR();
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlite(Configuration.GetConnectionString("ConnectSqlite")));
builder.Services.AddAuthentication()
.AddCookie("AdminCookie", options =>
{
    options.Cookie.Name = "Basic.Admin.Cookie";
    options.ExpireTimeSpan = TimeSpan.FromHours(1);
    options.LoginPath = "/Admin/Login";
})
.AddCookie("ClientCookie",options=>{
    options.Cookie.Name ="Basic.Client.Cookie";
    options.ExpireTimeSpan = TimeSpan.FromHours(1);
    options.LoginPath="/Home/Login";
});

builder.Services.AddHostedService<AutoUpdateStockBackgroundTask>();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<StockHubConnectionManager>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();
app.UseAuthentication();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapHub<AdminHub>("/adminHub");
app.MapHub<StockHub>("/stockHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);
app.MigrateDbContext<AppDbContext>((context, services) =>
{
    var logger = services.GetRequiredService<ILogger<AppDbContextSeed>>();
    new AppDbContextSeed()
        .SeedAsync(context, logger)
        .Wait();
});
app.Run();
