using ApiCatalog.Context;
using ApiCatalog.Endpoints.Categories;
using ApiCatalog.Endpoints.Products;
using ApiCatalog.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); // Get the connection string from appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>  // Add the database context
    options.UseSqlServer(connectionString)
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.MapCategoriesEndpoints();
app.MapProductsEndpoints();

app.Run();
