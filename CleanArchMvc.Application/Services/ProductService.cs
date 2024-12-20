using System;
using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using MediatR;

namespace CleanArchMvc.Application.Services;

public class ProductService : IProductService
{
    private IMapper _mapper;
    private IMediator _mediator;

    public ProductService(IMapper mapper, IMediator mediator)
    {
        _mediator = mediator ??
            throw new ArgumentException(nameof(mediator));
        _mapper = mapper;
    }

    public async Task Add(ProductDTO productDTO)
    {
        var productCreateCommand = _mapper.Map<ProductCreateCommand>(productDTO);
        await _mediator.Send(productCreateCommand);
    }

    public async Task<ProductDTO> GetById(int id)
    {
        var productsQuery = new GetProductByIdQuery(id);
        if(productsQuery == null)
            throw new Exception($"Entity could not be loaded.");

        var result = await _mediator.Send(productsQuery);
        return _mapper.Map<ProductDTO>(result);
    }

    public async Task<IEnumerable<ProductDTO>> GetProductCategoryAsync(int id)
    {
        var productsQuery = new GetProductByIdQuery(id);
        if(productsQuery == null)
            throw new Exception($"Entity could not be loaded.");

        var result = await _mediator.Send(productsQuery);
        return _mapper.Map<IEnumerable<ProductDTO>>(result);
    }

    public async Task<IEnumerable<ProductDTO>> GetProducts()
    {
        var productsQuery = new GetProductsQuery();
        if(productsQuery == null)
            throw new Exception($"Entity could not be loaded.");
        
        var result = await _mediator.Send(productsQuery);
        return  _mapper.Map<IEnumerable<ProductDTO>>(result);
    }

     public async Task Remove(int id)
    {
        var productRemoveCommand = new ProductRemoveCommand(id);
        if (productRemoveCommand == null)
            throw new Exception($"Entity could not be loaded.");

        await _mediator.Send(productRemoveCommand);
    }

    public async Task Update(ProductDTO productDTO)
    {
        var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDTO);
        await _mediator.Send(productUpdateCommand);
    }
}