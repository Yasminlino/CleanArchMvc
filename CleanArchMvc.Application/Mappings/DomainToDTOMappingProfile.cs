using System;
using AutoMapper;
using CleanArchMvc.Application.DTO;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Application.Mappings;

public class DomainToDTOMappingProfile : Profile
{
    public DomainToDTOMappingProfile()
    {
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<Product, ProductDTO>().ReverseMap();
    }
}
