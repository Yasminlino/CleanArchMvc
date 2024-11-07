using System;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Domain.Interfaces;

public interface IProductRepository
{
    // Retorna uma lista assíncrona de produtos.
    Task<IEnumerable<Product>> GetProductAsync();

    // Retorna uma produto pelo seu ID, se existir.
    Task<Product> GetById(int? id);
    Task<IEnumerable<Product>> GetProductCategoryAsync(int? id);

    // Cria uma nova produto e retorna a produto criada.
    Task<Product> Create(Product product);

    // Atualiza uma produto existente e retorna a produto atualizada.
    Task<Product> Update(Product Product);

    // Remove uma produto e retorna a produto removida.
    Task<Product> Remove(Product Product);
    Task<Product> CreateAsync(Product product);
    Task<Product> GetByIdAsync(int id);
    Task<Product> UpdateAsync(object product);
    Task<Product> RemoveAsync(Task<Product> product);
}
