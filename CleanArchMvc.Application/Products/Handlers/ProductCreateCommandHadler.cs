using System;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers;

public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, Product>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductCreateCommandHandler(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<Product> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
        Category category = await _categoryRepository.GetByIdAsync(request.CategoryId);
        if(category == null)
            throw new ArgumentException("Categoria não encontrada.");

        
        var product = new Product(request.Name, request.Description, request.Price, request.Stock, request.Image, request.CategoryId, category);

        if(product == null)
        {
            throw new ApplicationException($"Error creating entity.");
        }
        else
        {
            return await _productRepository.CreateAsync(product);
        }
    }
}
