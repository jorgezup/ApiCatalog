using ApiCatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalog.Context;

public class AppDbContext : DbContext // DbContext is a class from Entity Framework Core
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } 

    public DbSet<Product>? Products { get; set; } // DbSet is a class from Entity Framework Core
    public DbSet<Category>? Categories { get; set; } 
}