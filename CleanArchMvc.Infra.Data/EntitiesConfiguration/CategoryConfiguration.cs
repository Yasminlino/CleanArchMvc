using System;
using CleanArchMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchMvc.Infra.Data.EntitiesConfiguration;
public class CategoryConfiguration : IEntityTypeConfiguration<Category> // Define a configuração da entidade 'Category'
{
    public void Configure(EntityTypeBuilder<Category> builder) // Método para configurar a entidade 'Category'
    {
        builder.HasKey(t => t.Id); // Define a propriedade 'Id' como a chave primária da entidade

        // Configura a propriedade 'Name' com um comprimento máximo de 100 caracteres e a torna obrigatória
        builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
        builder.HasData(
            new Category(1, "Material Escolar"),
            new Category(2, "Eletrônicos"),
            new Category(3, "Acessórios")
        );
    }
}
