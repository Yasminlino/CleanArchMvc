using System;
using CleanArchMvc.Application.DTO;

namespace CleanArchMvc.Application.Interfaces;

public interface ICategoryService
{
    Task Add(CategoryDTO categoryDTO); //Adiciona uma nova categoryDTO
    Task<IEnumerable<CategoryDTO>> GetCategories(); //Retorna uma lista de category DTO
    Task<IEnumerable<CategoryDTO>> GetById(int? id); //Retorna pelo Id
    Task Remove(int? id); //Remove uma categoryDTO pelo Id
    Task Update(CategoryDTO categoryDTO); //Atualiza uma nova categoryDTO

}
