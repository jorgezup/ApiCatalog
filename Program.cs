using ApiCatalog.Context;
using ApiCatalog.Endpoints.Categories;
using ApiCatalog.Endpoints.Products;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); // Get the connection string from appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>  // Add the database context
    options.UseMySql(connectionString, 
        ServerVersion.AutoDetect(connectionString) // Detect the server version
    )
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapCategoriesEndpoints();
app.MapProductsEndpoints();

app.Run();
