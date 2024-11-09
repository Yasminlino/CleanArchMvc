using System;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Products.Handlers;

public class ProductCreateCommandHadler : IRequestHandler<ProductCreateCommand, Product>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;

    public ProductCreateCommandHadler(IProductRepository productRepository, ICategoryRepository categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<Product> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
    
        var product = new Product(request.Name, request.Description, request.Price, request.Stock, request.Image, request.CategoryId, request.Category);

        if(product == null)
        {
            throw new ApplicationException($"Error creating entity.");
        }
        else
        {
            product.CategoryId = request.CategoryId;
            return await _productRepository.CreateAsync(product);
        }
    }
}
