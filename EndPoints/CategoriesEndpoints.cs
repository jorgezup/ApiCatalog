using ApiCatalog.Context;
using ApiCatalog.Models;

namespace ApiCatalog.EndPoints;

public static class CategoriesEndpoints
{
    public static void MapCategoriesEndpoints(this WebApplication app)
    {
        app.MapGet("/categories",  (AppDbContext db) =>
        {
            if (db.Categories is null)
                return Results.NotFound();
            
            var categories = db.Categories.ToList();
            return Results.Ok(categories);
        });
        
        app.MapPost("/categories", async (AppDbContext db, Category category) =>
        {
            if (db.Categories != null) 
                await db.Categories.AddAsync(category);
            
            await db.SaveChangesAsync();
            return Results.Created($"/categories/{category.CategoryId}", category);
        });

    }
}