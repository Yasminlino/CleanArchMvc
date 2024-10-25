using CleanArchMvc.Infra.Ioc;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Configurations;
using Microsoft.Extensions.DependencyInjection;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddInfrastructure(builder.Configuration);

// Não é necessário configurar o MongoClient aqui


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedDatabase(services);
}

async Task SeedDatabase(IServiceProvider services)
{
     var categoryRepository = services.GetRequiredService<ICategoryRepository>();
    var productRepository = services.GetRequiredService<IProductRepository>();
    
    // Verifica se as categorias já existem
    if (!(await categoryRepository.GetCategories()).Any())
    {
        // Adiciona as categorias
        await categoryRepository.Create(new Category(1, "Material Escolar"));
        await categoryRepository.Create(new Category(2, "Eletrônicos"));
        await categoryRepository.Create(new Category(3, "Acessórios"));
    }
    if (!(await productRepository.GetProductAsync()).Any())
    {
        // Adiciona as categorias
        await productRepository.Create(new Product(1, "Caderno Espiral", "Caderno Espiral 100 paginas",27.5, 50,"img.jpg", 1));
        await productRepository.Create(new Product(2, "Caderno Desenho", "Caderno Desenho 100 paginas",17.5, 50,"img.jpg", 1));
        await productRepository.Create(new Product(3, "Calculadora", "Calculadora Cientifica",300.00, 50,"img.jpg", 2));
        await productRepository.Create(new Product(4, "Pingente", "Pingente Coração",10.5, 50,"img.jpg", 3));
        await productRepository.Create(new Product(5, "Cola Bastão", "Cola bastão 40g",8.00, 50,"img.jpg", 3));
    }
}


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
