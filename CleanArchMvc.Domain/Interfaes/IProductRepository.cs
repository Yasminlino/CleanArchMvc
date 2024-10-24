using System;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Domain.Interfaces;

public interface IProductRepository
{
    // Retorna uma lista assíncrona de produtos.
    Task<IEnumerable<Product>> GetProductAsync();

    // Retorna uma produto pelo seu ID, se existir.
    Task<Product> GetById(int? id);

    // Cria uma nova produto e retorna a produto criada.
    Task<Product> Create(Product product);

    // Atualiza uma produto existente e retorna a produto atualizada.
    Task<Product> Update(Product Product);

    // Remove uma produto e retorna a produto removida.
    Task<Product> Remove(Product Product);

}
