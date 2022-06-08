using Exam01.Application.Models;
using Exam01.Database.EntityTypeConfiguration;
using Exam01.Models;
using Microsoft.EntityFrameworkCore;

namespace Exam01.Database;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}
    public DbSet<User> Users {get;set;}
    public DbSet<Stock> Stocks{get;set;}
    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.ApplyConfiguration(new StockTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
    }
}
