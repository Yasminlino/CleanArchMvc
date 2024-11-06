using System;
using AutoMapper;
using CleanArchMvc.Application.DTO;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services;

public class ProductService : IProductService
{
    private IProductRepository _productRepository;
    private ICategoryRepository _categoryRepository;
    private IMapper _mapper;

    public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
    {
        _productRepository = productRepository ??
            throw new ArgumentException(nameof(productRepository));
        _categoryRepository = categoryRepository; 
        _mapper = mapper;
    }

    public async Task Add(ProductDTO productDTO)
    {
        var productEntity = _mapper.Map<Product>(productDTO);
        await _productRepository.Create(productEntity);
    }

    public async Task<IEnumerable<ProductDTO>> GetById(int? id)
    {
        var productEntity = await _productRepository.GetById(id);
        return _mapper.Map<IEnumerable<ProductDTO>>(productEntity);
    }

    public async Task<IEnumerable<ProductDTO>> GetProductCategory(int? id)
    {
        var productEntity = await _productRepository.GetProductCategoryAsync(id);
        return  _mapper.Map<IEnumerable<ProductDTO>>(productEntity);

    }

    public async Task<IEnumerable<ProductDTO>> GetProducts()
    {
        var productEntity = await _productRepository.GetProductAsync();
        return _mapper.Map<IEnumerable<ProductDTO>>(productEntity);
    }

    public async Task Remove(ProductDTO productDTO)
    {
        var productEntity = _mapper.Map<Product>(productDTO);
        await _productRepository.Remove(productEntity);
    }

    public async Task Update(ProductDTO productDTO)
    {
        var productEntity = _mapper.Map<Product>(productDTO);
        await _productRepository.Update(productEntity);
    }
}
