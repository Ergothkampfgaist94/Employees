using Employees.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Employees.Backend.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Employee>().HasIndex(x => x.Id).IsUnique();
        modelBuilder.Entity<Employee>().Property(x => x.Salary).HasPrecision(18, 2);
    }
}