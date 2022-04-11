using GasGuru.Database.Configurations;
using GasGuru.Entities;
using Microsoft.EntityFrameworkCore;

namespace GasGuru.Database;

internal class GasStationContext : DbContext
{
    public DbSet<Customer> Customers { get; } = null!;
    public DbSet<Item> Items { get; } = null!;
    public DbSet<Employee> Employees { get; } = null!;
    public DbSet<Transaction> Transactions { get; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer($"Data Source=(localdb)\\MSSQLLocalDB;Database={nameof(GasGuru)};");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new ItemConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionLineConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
