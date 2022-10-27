using ApiCatalog.Context;
using ApiCatalog.Endpoints.Categories;
using ApiCatalog.Models;

namespace ApiCatalog.EndPoints;

public static class CategoriesEndpoints
{
    public static void MapCategoriesEndpoints(this WebApplication app)
    {
        app.MapGet("/categories",  (AppDbContext db) =>
        {
            try
            {
                if (db.Categories is null)
                    return Results.NotFound();
            
                var categories = db.Categories.ToList();
                return Results.Ok(categories);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        });
        
        app.MapPost("/categories", async (AppDbContext db, CategoryRequest  categoryRequest) =>
        {
            try
            {
                var category = new Category(categoryRequest.Name, categoryRequest.Description);
                
                if (db.Categories is null)
                    return Results.NotFound();
                
                await db.Categories.AddAsync(category);
                await db.SaveChangesAsync();
                
                return Results.Created($"/categories/{category.CategoryId}", category);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        });

    }
}