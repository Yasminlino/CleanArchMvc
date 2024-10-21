using System;
using CleanArchMvc.Domain.Entities;
using FluentAssertions;
using Xunit;
namespace CleanArchMvc.Domain.Tests;

public class CategoryUnit
{
    [Fact(DisplayName ="Create Category With Valid State")] //Criar categoria com estado válido
    public void CreateCategory_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => new Category("Category Name");
        action.Should()
            .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>(); // Não lança uma exceção
    }

    [Fact(DisplayName ="Create Category Negative Id Value")] //Criar categoria com sucesso
    public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () => new Category(-1, "Category Name");
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid Id Value");
    }

    [Fact(DisplayName ="Create Category Short Name Value")] //Criar categoria com menos de 3 caracteres
    public void CreateCategory_ShortNameValue_DomainExceptionShortName()
    {
        Action action = () => new Category(1, "Ca");
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid name, too short, minimum 3 characters");
    }

    [Fact(DisplayName ="Create Category Missing Name Value")] //Criar categoria com nome vazio
    public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
    {
        Action action = () => new Category(1, "");
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid name. Name is required");
    }
}
