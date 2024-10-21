using System;
using CleanArchMvc.Domain.Entities;
using FluentAssertions;
using Xunit;
namespace CleanArchMvc.Domain.Tests;

public class ProductUnit
{
    [Fact(DisplayName ="Create Product With Valid State")] //Criar Produto com sucesso
    public void CreateProduct_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => new Product("Product Name", "description", 10.50, 1, "image");
        action.Should()
            .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>(); // Não lança uma exceção
    }

    [Fact(DisplayName ="Create Product Negative Id Value")] //Criar Produto id negativo
    public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () => new Product(-1, "Product Name", "description", 10.50, 1, "image");
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid Id Value");
    }

    [Fact(DisplayName ="Create Product Missing Name Value")] //Criar Produto com nome vazio
    public void CreateProduct_MissingNameValue_DomainExceptionInvalidId()
    {
        Action action = () => new Product(1, "", "description", 10.50, 1, "image");
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid name. Name is required");
    }

    [Fact(DisplayName ="Create Product Short Name Value")] //Criar Produto com o nome menos de 3 caracteres
    public void CreateProduct_ShortNameValue_ResultObjectValidState()
    {
        Action action = () => new Product("Pr", "description", 10.50, 1, "image");
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid name, too short, minimum 3 characters");
    }

    [Fact(DisplayName ="Create Product Missing Name Value")] //Criar Produto com descrição vazia
    public void CreateProduct_MissingDescriptionValue_DomainExceptionInvalidId()
    {
        Action action = () => new Product(1, "Product Name", "", 10.50, 1, "image");
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid description. Description is required");
    }

    [Fact(DisplayName ="Create Product Short Description Value")] //Criar Produto com o descricao com menos de 5 caracteres
    public void CreateProduct_ShortDescriptionValue_ResultObjectValidState()
    {
        Action action = () => new Product("Product Name", "desc", 10.50, 1, "image");
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid description, too short, minimum 5 characters");
    }

    [Fact(DisplayName ="Create Product Invalid Price Value")] //Criar Produto com o preco NEGATIVO
    public void CreateProduct_InvalidPriceValue_ResultObjectValidState()
    {
        Action action = () => new Product("Product Name", "descricao", -1, 1, "image");
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid price value");
    }

    [Theory(DisplayName = "Create Product Invalid Stock Value")] // Criar Produto com o estoque NEGATIVO // Theory Utilizado quando possui parâmetro
    [InlineData(-5)] // Atributo pode ser passado assim
    public void CreateProduct_InvalidStockValue_ResultObjectValidState(int value)
    {
        // Arrange
        Action action = () => new Product("Product Name", "descricao", 10.50, value, "image"); // Use o valor passado

        // Act & Assert
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid stock value");
    }


    [Fact(DisplayName ="Create Product Long Image Value")] //Criar Produto com imagem longa + de 250 caacteres
    public void CreateProduct_LongImageValue_DomainExceptionInvalidId()
    {
        var image = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
        +  "eeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee"
        + "iiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii"
        + "ooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo"
        + "uuuuuuuuuuuuuuuuuuuuuuuuuuuu";
        Action action = () => new Product(1, "Product Name", "description", 10.50, 1, image);
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid image name, too long, maximum 250 characters");
    }

    [Fact(DisplayName ="Create Product With Null Image Name")] //Criar Produto com sucesso
    public void CreateProduct_WithNullImageName_DomainExceptionInvalidId()
    {
        Action action = () => new Product(1, "Product Name", "description", 10.50, 1, null);
        action.Should()
            .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
    }

    [Fact(DisplayName ="Create Product With Null Image Name")] //Criar Produto com sucesso
    public void CreateProduct_WithNullImageName_DomainExceptionInvalid()
    {
        Action action = () => new Product(1, "Product Name", "description", 10.50, 1, null);
        action.Should()
            .NotThrow<NullReferenceException>();
    }

    [Fact(DisplayName ="Create Product With Empty Image Name")] //Criar Produto com sucesso
    public void CreateProduct_WithEmptyImageName_DomainExceptionInvalidId()
    {
        Action action = () => new Product(1, "Product Name", "description", 10.50, 1, "");
        action.Should()
            .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
    }
}

