using System;
using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces;

public interface ICategoryService
{
    Task Add(CategoryDTO categoryDTO); //Adiciona uma nova categoryDTO
    Task<IEnumerable<CategoryDTO>> GetCategories(); //Retorna uma lista de category DTO
    Task<CategoryDTO> GetByIdAsync(int? id); //Retorna pelo Id
    Task Remove(int? id); //Remove uma categoryDTO pelo Id
    Task Update(CategoryDTO categoryDTO); //Atualiza uma nova categoryDTO

}
