namespace ApiCatalog.Models;

public class Product
{
    public Guid ProductId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? Image { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastUpdated { get; set; }
    public int Stock { get; set; }
    
    public Guid CategoryId { get; set; } // FK
    public Category? Category { get; set; } // Navigation property
}