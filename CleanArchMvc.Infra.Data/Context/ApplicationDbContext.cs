using System;
using CleanArchMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMvc.Application.Context;
// Classe que representa o contexto da aplicação, que é responsável pela interação com o banco de dados
public class ApplicationDbContext : DbContext
{
    // Construtor que recebe opções de configuração para o DbContext
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) // Chama o construtor da classe base (DbContext) com as opções fornecidas
    { }

    // DbSet para representar a tabela de categorias no banco de dados
    public DbSet<Category> Categories { get; set; }

    // DbSet para representar a tabela de produtos no banco de dados
    public DbSet<Product> Products { get; set; }

    // Método que permite configurar o modelo durante a criação do contexto
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Chama a implementação base para garantir que a configuração padrão do modelo seja aplicada
        base.OnModelCreating(builder);
        
        // Aplica todas as configurações de mapeamento de entidades encontradas no assembly
        // Isso permite que as configurações sejam organizadas em classes separadas
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
