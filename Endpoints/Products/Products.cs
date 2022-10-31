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
        
        app.MapGet("/products/{id:Guid}", (AppDbContext db, Guid id) =>
        {
            try
            {
                if (db.Products is null)
                    return Results.NotFound();

                var product = db.Products.FirstOrDefault(p => p.ProductId == id);
                if (product is null)
                    return Results.NotFound();

                return Results.Ok(product);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }).WithTags("Products")
            .Produces<Product>();
        
        app.MapPut("/products/{id:Guid}", async (AppDbContext db, Guid id, ProductRequest productRequest) =>
        {
            try
            {
                if (db.Products is null)
                    return Results.NotFound();

                var product = db.Products.FirstOrDefault(p => p.ProductId == id);
                if (product is null)
                    return Results.NotFound();

                product.Name = productRequest.Name;
                product.Description = productRequest.Description;
                product.Price = productRequest.Price;
                product.Image = productRequest.Image;
                product.CategoryId = productRequest.categoryId;
                
                db.Products.Update(product);
                await db.SaveChangesAsync();
                return Results.Ok(product);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        }).WithTags("Products")
            .Produces<Product>();
        
        app.MapDelete("/products/{id:Guid}", async (AppDbContext db, Guid id) =>
        {
            try
            {
                if (db.Products is null)
                    return Results.NotFound();

                var product = db.Products.FirstOrDefault(p => p.ProductId == id);
                if (product is null)
                    return Results.NotFound();

                db.Products.Remove(product);
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