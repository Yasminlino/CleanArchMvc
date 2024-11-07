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

        // Registre seus repositórios
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

        var myhandlers = AppDomain.CurrentDomain.Load("CleanArchMvc.Application");
        services.AddMediatR(myhandlers);

        return services;
    }
}
