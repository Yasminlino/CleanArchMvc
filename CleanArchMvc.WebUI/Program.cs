using CleanArchMvc.Infra.Ioc;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Configurations;
using Microsoft.Extensions.DependencyInjection;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Application.Mappings;
using CleanArchMvc.Infra.Data.Identity;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(DomainToDTOMappingProfile));


// Conectar ao MongoDB

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true; // Exige um dígito
    options.Password.RequireLowercase = true; // Exige letras minúsculas
    options.Password.RequireUppercase = true; // Exige letras maiúsculas
    options.Password.RequireNonAlphanumeric = true; // Exige caracteres especiais
    options.Password.RequiredLength = 8; // Exige no mínimo 8 caracteres
    options.Password.RequiredUniqueChars = 1; // Exige pelo menos 1 caractere único
});


builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

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

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedDatabase(services);
}

async Task SeedDatabase(IServiceProvider services)
{
    var categoryRepository = services.GetRequiredService<ICategoryRepository>();
    var productRepository = services.GetRequiredService<IProductRepository>();
    var seedUserRoleInitial = services.GetRequiredService<SeedUserRoleInitial>();

    seedUserRoleInitial.SeedRoles();
    seedUserRoleInitial.SeedUsers();
    
    // Verifica se as categorias já existem
    if (!(await categoryRepository.GetCategories()).Any())
    {
        await categoryRepository.Create(new Category(1, "Material Escolar"));
        await categoryRepository.Create(new Category(2, "Eletrônicos"));
        await categoryRepository.Create(new Category(3, "Acessórios"));
    }

    if (!(await productRepository.GetProductAsync()).Any())
    {
        // Adiciona as categorias
        // await productRepository.Create(new Product(1, "Caderno Espiral", "Caderno Espiral 100 paginas",27.5, 50,"img.jpg", 1));
        // await productRepository.Create(new Product(2, "Caderno Desenho", "Caderno Desenho 100 paginas",17.5, 50,"img.jpg", 1));
        // await productRepository.Create(new Product(3, "Calculadora", "Calculadora Cientifica",300.00, 50,"img.jpg", 2));
        // await productRepository.Create(new Product(4, "Pingente", "Pingente Coração",10.5, 50,"img.jpg", 3));
        // await productRepository.Create(new Product(5, "Cola Bastão", "Cola bastão 40g",8.00, 50,"img.jpg", 3));
    }
}

app.Run();
