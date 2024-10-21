using System;
using CleanArchMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchMvc.Infra.Data.EntitiesConfiguration;
public class ProductConfiguration : IEntityTypeConfiguration<Product> // Define a configuração da entidade 'Product'
{
    public void Configure(EntityTypeBuilder<Product> builder) // Método para configurar a entidade 'Product'
    {
        builder.HasKey(t => t.Id); // Define a propriedade 'Id' como a chave primária da entidade

        // Configura a propriedade 'Name' com um comprimento máximo de 100 caracteres e a torna obrigatória
        builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
        
        // Configura a propriedade 'Description' com um comprimento máximo de 200 caracteres e a torna obrigatória
        builder.Property(p => p.Description).HasMaxLength(200).IsRequired();

        // Configura a propriedade 'Price' com precisão de 10 dígitos no total e 2 casas decimais
        builder.Property(p => p.Price).HasPrecision(10, 2);
        
        // Configura o relacionamento entre 'Product' e 'Category'
        // Um produto pertence a uma categoria, e uma categoria pode ter muitos produtos
        builder.HasOne(p => p.Category).WithMany(e => e.Products).HasForeignKey(e => e.CategoryId);
    }
}

