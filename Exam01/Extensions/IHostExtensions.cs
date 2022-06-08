using Microsoft.EntityFrameworkCore;

namespace BDRD.DemoCICD.CRUDAPP.Web.Extensions;
public static class IHostExtensions
{
    public static IHost MigrateDbContext<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder)
    where TContext : DbContext
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<TContext>>();
            var context = services.GetService<TContext>();
            try
            {
                logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);
                InvokeSeeder(seeder, context, services);
                logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);
            }
        }
        return host;
    }
    private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services)
    where TContext : DbContext
    {
        if (context != null)
        {
            try
            {
                context.Database.EnsureCreated();
            }
            finally
            {
                seeder(context, services);
            }
        }
    }

}