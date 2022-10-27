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
                var categoriesResponse = categories.Select(c 
                    => new CategoryResponse(c.CategoryId, c.Name!, c.Description!));
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
        
        app.MapGet("/categories/{id:Guid}", (AppDbContext db, Guid id) =>
        {
            try
            {
                if (db.Categories is null)
                    return Results.NotFound();
                
                var category = db.Categories.FirstOrDefault(c => c.CategoryId == id);
                
                if (category is null)
                    return Results.NotFound();
                
                var categoryResponse = new CategoryResponse(category.CategoryId, category.Name!, category.Description!);
                
                return Results.Ok(categoryResponse);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        });
        
        app.MapPut("/categories/{id:Guid}", async (AppDbContext db, Guid id, CategoryRequest categoryRequest) =>
        {
            try
            {
                if (db.Categories is null)
                    return Results.NotFound();
                
                var category = db.Categories.FirstOrDefault(c => c.CategoryId == id);
                
                if (category is null)
                    return Results.NotFound();
                
                category.Name = categoryRequest.Name;
                category.Description = categoryRequest.Description;
                
                db.Categories.Update(category);
                await db.SaveChangesAsync();
                
                return Results.Ok(category);
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        });
        
        app.MapDelete("/categories/{id:Guid}", async (AppDbContext db, Guid id) =>
        {
            try
            {
                if (db.Categories is null)
                    return Results.NotFound();
                
                var category = db.Categories.FirstOrDefault(c => c.CategoryId == id);
                
                if (category is null)
                    return Results.NotFound();
                
                db.Categories.Remove(category);
                await db.SaveChangesAsync();
                
                return Results.NoContent();
            }
            catch (Exception e)
            {
                return Results.BadRequest(e.Message);
            }
        });

    }
}