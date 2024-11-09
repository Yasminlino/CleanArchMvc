using System;
using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.Application.Interfaces;

public interface IProductService
{
    Task Add(ProductDTO productDTO);
    Task<ProductDTO> GetById(int id);
    Task<IEnumerable<ProductDTO>> GetProductCategoryAsync(int id);
    Task<IEnumerable<ProductDTO>> GetProducts();
    Task Remove(int id);
    Task Update(ProductDTO productDTO);
}
