using Microsoft.EntityFrameworkCore;
using WebApplication6.Data;
using WebApplication6.Models;
using WebApplication6.Repositories;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Add services to the container.

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await SeedDataAsync(app);

app.Run();

async Task SeedDataAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>(); 

    await context.Database.MigrateAsync();

    if (!context.Categories.Any())
    {
        var categories = new[]
        {
            new Category { Name = "Electronics" },
            new Category { Name = "Books" },
            new Category { Name = "Clothing" }
        };

        await context.Categories.AddRangeAsync(categories);
        await context.SaveChangesAsync();

        var electronics = categories.First(c => c.Name == "Electronics");
        var books = categories.First(c => c.Name == "Books");
        var clothing = categories.First(c => c.Name == "Clothing");
        var products = new[]
        {
            new Product { Name = "T-shirt", Price = 1000.50m, CategoryId = clothing.CategoryId, Category = clothing },
            new Product { Name = "1984", Price = 600.90m, CategoryId = books.CategoryId, Category = books },
            new Product { Name = "Laptop", Price = 35000.75m, CategoryId = electronics.CategoryId, Category = electronics }
        };

        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();

        Console.WriteLine("Данные успешно добавлены в базу.");
    }
    else
    {
        Console.WriteLine("В базе данных уже есть данные.");
    }
}

