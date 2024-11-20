using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Repositories;
using CleanArchMvc.Infra.Data.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using CleanArchMvc.Application.Mappings;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Services;
using MediatR;
using CleanArchMvc.Infra.Data.Identity;
using Microsoft.AspNetCore.Identity;
using CleanArchMvc.Infra.Data.Context;
using CleanArchMvc.Domain.Account;
namespace CleanArchMvc.Infra.Ioc;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Configuração do MongoDB
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));

        // Registro do cliente MongoDB
        services.AddSingleton<IMongoClient>(serviceProvider =>
        {
            var settings = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            return new MongoClient(settings.ConnectionString);
        });

        // Registro do IMongoDatabase
        services.AddSingleton<IMongoDatabase>(serviceProvider =>
        {
            var settings = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            var client = serviceProvider.GetRequiredService<IMongoClient>();
            return client.GetDatabase(settings.DatabaseName); // Nome do banco de dados configurado no appsettings.json
        });

        // Agora o MongoDbContext irá pegar a instância de IMongoDatabase já registrada.
        services.AddScoped<MongoDbContext>();

        // Registre seus repositórios e outros serviços
        services.AddScoped<IRoleStore<IdentityRole>, MongoRoleStore>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<SeedUserRoleInitial>(); // Registro de SeedUserRoleInitial
        services.AddScoped<IAuthenticate, AuthenticateService>();
        services.AddScoped<IUserStore<ApplicationUser>, MongoUserStore>();


        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

        var myhandlers = AppDomain.CurrentDomain.Load("CleanArchMvc.Application");
        services.AddMediatR(myhandlers);

        return services;
    }
}

