using System;
using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities;

public sealed class Product : Entity //sealed definem que a classe não pode ser herdada //O Entity é o Id do product
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public double Price { get; private set; }
    public int Stock { get; private set; }
    public string Image { get; private set; }

    //Propriedade de navegação
    public int CategoryId { get; set; } //Chave estrangeira (propriedade de categoria do produto)
    public Category? Category { get; set; } //Propriedade que relaciona o produto com a category

    public Product(int id, string name, string description, double price, int stock, string image, int categoryId, Category category)
    {
        DomainExceptionValidation.When(id < 0, "Invalid Id value");
        Id = id;
        ValidateDomain(name, description, price, stock, image, categoryId, category);
    }
    public Product(string name, string description, double price, int stock, string image, int categoryId,Category category)
    {
        ValidateDomain(name, description, price, stock, image, 0, category);
    }

    public void Update(string name, string description, double price, int stock, string image, int categoryId)
    {
        ValidateDomain(name, description, price, stock, image);
        CategoryId = categoryId;
    }

    public void ValidateDomain(string name, string description, double price, int stock, string? image, int categoryId = 0, Category category = null)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(name),
            "Invalid name. Name is required");

        DomainExceptionValidation.When(name.Length < 3,
            "Invalid name, too short, minimum 3 characters");
        
        DomainExceptionValidation.When(string.IsNullOrEmpty(description),
            "Invalid description. Description is required");
        
        DomainExceptionValidation.When(description.Length < 5,
            "Invalid description, too short, minimum 5 characters");

        DomainExceptionValidation.When(price < 0, "Invalid price value");

        DomainExceptionValidation.When(stock < 0, "Invalid stock value");

        DomainExceptionValidation.When(image?.Length > 250,
            "Invalid image name, too long, maximum 250 characters");
        
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        Image = image;
        CategoryId = categoryId;
        Category = category;
    }
}
