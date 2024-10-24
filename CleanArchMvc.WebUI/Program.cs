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
    
    // Verifica se as categorias já existem
    if (!(await categoryRepository.GetCategories()).Any())
    {
        // Adiciona as categorias
        await categoryRepository.Create(new Category(1, "Material Escolar"));
        await categoryRepository.Create(new Category(2, "Eletrônicos"));
        await categoryRepository.Create(new Category(3, "Acessórios"));
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
