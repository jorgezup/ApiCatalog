using System.Text.Json.Serialization;

namespace ApiCatalog.Models;

public class Category
{
    public Guid CategoryId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    
    [JsonIgnore] // Prevents infinite recursion
    public ICollection<Product>? Products { get; set; } // 1 to many

    public Category(string name, string description)
    {
        CategoryId = Guid.NewGuid();
        Name = name;
        Description = description;
    }
}