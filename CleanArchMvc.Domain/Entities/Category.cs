using System;
using CleanArchMvc.Domain.Validation;

namespace CleanArchMvc.Domain.Entities;

public sealed class Category : Entity //sealed definem que a classe não pode ser herdada
{
    public string Name { get; private set; }

    public Category(string name){
        ValidateDomain(name);
    }
    public Category(int id, string name){
        DomainExceptionValidation.When(id < 0, "Invalid Id Value");
        Id = id;
        ValidateDomain(name);
    }

    public void Update(string name)
    {
        ValidateDomain(name);
    }

    public ICollection<Product> Products {get; set;} //Uma categoria pode ter um ou mais produtos

    private void ValidateDomain(string name)
    {
        DomainExceptionValidation.When(string.IsNullOrEmpty(name),
            "Invalid name.Name is required");

        DomainExceptionValidation.When(name.Length < 3,
            "Invalid name, too short, minimum 3 characters");

        Name = name;
    }

}
