using Microsoft.EntityFrameworkCore;
using InvoicingSystem.Api.Models;

namespace InvoicingSystem.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext>options) : base(options)
    {}

    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Invoice>()
        .Property(i => i.Amount)
        .HasPrecision(18, 2);

    modelBuilder.Entity<Payment>()
        .Property(p => p.Amount)
        .HasPrecision(18, 2);
}
}