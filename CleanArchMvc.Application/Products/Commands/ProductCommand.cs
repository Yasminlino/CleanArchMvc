using System;
using CleanArchMvc.Domain.Entities;
using MediatR;

namespace CleanArchMvc.Application.Products.Commands;

public abstract class ProductCommand : IRequest<Product>
{
    public string? Name { get; private set; }
    public string? Description { get; private set; }
    public double Price { get; private set; }
    public int Stock { get; private set; }
    public string? Image { get; private set; }
    public int CategoryId { get; set; } //Chave estrangeira (propriedade de categoria do produto)
    public Category? Category { get; set; } //Propriedade que relaciona o produto com a category

}
