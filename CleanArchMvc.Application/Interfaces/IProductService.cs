using System;
using CleanArchMvc.Application.DTO;

namespace CleanArchMvc.Application.Interfaces;

public interface IProductService
{
    Task Add(ProductDTO productDTO);
    Task<IEnumerable<ProductDTO>> GetById(int? id);
    Task<IEnumerable<ProductDTO>> GetProductCategory(int? id);
    Task<IEnumerable<ProductDTO>> GetProducts();

    Task Remove(ProductDTO productDTO);
    Task Update(ProductDTO productDTO);
}
