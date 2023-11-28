using Microsoft.EntityFrameworkCore;
using inventory.Entities;

namespace inventory.Helpers;

public class DataContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<ProductMasuk> ProductMasuks { get; set; }
    public DbSet<ProductKeluar> ProductKeluars { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Category> Categories { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductMasuk>()
        .HasOne(p => p.Product)
        .WithMany()
        .HasForeignKey(p => p.ProductID);

        modelBuilder.Entity<ProductMasuk>()
            .HasOne(p => p.Supplier)
            .WithMany()
            .HasForeignKey(p => p.SupplierID);

        modelBuilder.Entity<ProductKeluar>()
            .HasOne(p => p.Product)
            .WithMany()
            .HasForeignKey(p => p.ProductID);

        modelBuilder.Entity<ProductKeluar>()
            .HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryID);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryID);

        base.OnModelCreating(modelBuilder);
    }


}