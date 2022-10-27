using ApiCatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalog.Context;

public class AppDbContext : DbContext // DbContext is a class from Entity Framework Core
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } 

    public DbSet<Product>? Products { get; set; } // DbSet is a class from Entity Framework Core
    public DbSet<Category>? Categories { get; set; } 
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) // OnModelCreating is a method from Entity Framework Core
    {
        // Fluent API
        // Category
        modelBuilder.Entity<Category>().HasKey(c => c.CategoryId);
        modelBuilder.Entity<Category>()
            .Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();
        modelBuilder.Entity<Category>()
            .Property(c => c.Description)
            .HasMaxLength(150)
            .IsRequired();
        
        // Product
        modelBuilder.Entity<Product>().Property(p => p.ProductId);
        modelBuilder.Entity<Product>()
            .Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();
        modelBuilder.Entity<Product>()
            .Property(p => p.Description)
            .HasMaxLength(150)
            .IsRequired();
        modelBuilder.Entity<Product>()
            .Property(p => p.Image)
            .HasMaxLength(100);
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(10,2)");
        
        // Relationship
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

    }
}