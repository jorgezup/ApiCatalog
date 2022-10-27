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

    public Product(string name, string description, decimal price, string image, Guid categoryId)
    {
        ProductId = Guid.NewGuid();
        Name = name;
        Description = description;
        Price = price;
        Image = image;
        Created = DateTime.Now;
        LastUpdated = DateTime.Now;
        Stock = 0;
        CategoryId = categoryId;
    }

}