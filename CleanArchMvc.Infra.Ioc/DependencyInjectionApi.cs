using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Mappings;
using CleanArchMvc.Application.Services;
using CleanArchMvc.Domain.Account;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Configurations;
using CleanArchMvc.Infra.Data.Context;
using CleanArchMvc.Infra.Data.Identity;
using CleanArchMvc.Infra.Data.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureApi(this IServiceCollection services, IConfiguration configuration)
    {
        // Configuração do MongoDB
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));

        // Registro do cliente MongoDB
        services.AddSingleton<IMongoClient>(serviceProvider =>
        {
            var settings = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            return new MongoClient(settings.ConnectionString); // Cria uma instância do cliente MongoDB
        });

        // Registro do IMongoDatabase
        services.AddSingleton<IMongoDatabase>(serviceProvider =>
        {
            var settings = serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            var client = serviceProvider.GetRequiredService<IMongoClient>();
            return client.GetDatabase(settings.DatabaseName); // Recupera a instância do banco de dados configurado
        });

        // Registra o contexto do MongoDB, que vai utilizar o banco de dados configurado
        services.AddScoped<MongoDbContext>();

        // Registro de Repositórios (dependências dos serviços)
        services.AddScoped<IRoleStore<IdentityRole>, MongoRoleStore>(); // RoleStore para Identity
        services.AddScoped<IProductRepository, ProductRepository>(); // Repositório de produtos
        services.AddScoped<ICategoryRepository, CategoryRepository>(); // Repositório de categorias
        services.AddScoped<SeedUserRoleInitial>(); // Serviço para inicialização de usuários e roles

        // Registro de serviços de autenticação
        services.AddScoped<IAuthenticate, AuthenticateService>(); // Serviço para autenticação
        services.AddScoped<IUserStore<ApplicationUser>, MongoUserStore>(); // Armazenamento de usuários do MongoDB

        // Registro dos serviços de aplicação
        services.AddScoped<IProductService, ProductService>(); // Serviço de negócios para produtos
        services.AddScoped<ICategoryService, CategoryService>(); // Serviço de negócios para categorias

        // Configuração do AutoMapper para mapeamento de DTOs
        services.AddAutoMapper(typeof(DomainToDTOMappingProfile)); // Mapeamento entre os objetos de domínio e DTOs

        // Registro do MediatR para enviar comandos e consultas entre as camadas
        var myhandlers = AppDomain.CurrentDomain.Load("CleanArchMvc.Application"); 
        services.AddMediatR(myhandlers); // Registro do MediatR

        return services; // Retorna o contêiner com os serviços configurados
    }
}
