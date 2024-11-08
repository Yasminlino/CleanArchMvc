using System;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Domain.Interfaces;

public interface ICategoryRepository
{
    // Retorna uma lista assíncrona de categorias.
    Task<IEnumerable<Category>> GetCategories();

    // Retorna uma categoria pelo seu ID, se existir.
    Task<Category> GetByIdAsync(int? id);

    // Cria uma nova categoria e retorna a categoria criada.
    Task<Category> Create(Category category);

    // Atualiza uma categoria existente e retorna a categoria atualizada.
    Task<Category> Update(Category category);

    // Remove uma categoria e retorna a categoria removida.
    Task<Category> Remove(Category category);
    Task<int> GetLastCategoryId();

}
