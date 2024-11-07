using System;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Domain.Interfaces;

public interface IProductRepository
{
    // Retorna uma lista assíncrona de produtos.
    Task<IEnumerable<Product>> GetProductAsync();

    // Retorna uma produto pelo seu ID, se existir.
    Task<Product> GetByIdAsync(int? id);

    // Task<IEnumerable<Product>> GetProductCategoryAsync(int? id);

    // Cria uma nova produto e retorna a produto criada.
    Task<Product> CreateAsync(Product product);

    // Atualiza uma produto existente e retorna a produto atualizada.
    Task<Product> UpdateAsync(Product Product);

    // Remove uma produto e retorna a produto removida.
    Task<Product> RemoveAsync(Product Product);
}
