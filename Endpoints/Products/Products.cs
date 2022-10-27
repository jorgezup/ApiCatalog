using ApiCatalog.Context;
using ApiCatalog.Models;

namespace ApiCatalog.Endpoints.Products;

public static class Products
{
    public static void MapProductsEndpoints(this WebApplication app)
    {
        app.MapGet("/products", (AppDbContext db) =>
        {
            try
            {
                if (db.Products is null)
                    return Results.NotFound();

                var products = db.Products.ToList();
                var productsResponse = products.Select(p => 
                    new ProductResponse(p.ProductId, p.Name!, p.Description!, p.Price, p.Image!));
                return Results.Ok(products);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }).WithTags("Products")
            .Produces<Product>();

        app.MapPost("/products", async (AppDbContext db, ProductRequest productRequest) =>
        {
            try
            {
                var product  = new Product(
                    productRequest.Name, productRequest.Description, 
                    productRequest.Price, productRequest.Image, productRequest.categoryId);
                
                if (db.Products is null)
                    return Results.NotFound();

                await db.Products.AddAsync(product);
                await db.SaveChangesAsync();
                return Results.Ok(product);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }).WithTags("Products")
            .Produces<Product>();
    }
}