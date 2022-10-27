namespace ApiCatalog.Endpoints.Products;

public record ProductRequest(string Name, string Description, decimal Price, string Image, Guid categoryId);