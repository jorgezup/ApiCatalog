namespace ApiCatalog.Endpoints.Products;

public record ProductResponse(Guid Id, string Name, string Description, decimal Price, string Image);