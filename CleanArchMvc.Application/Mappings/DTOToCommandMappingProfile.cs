using System;
using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Application.Mappings;

public class DTOToCommandMappingProfile : Profile
{
    public DTOToCommandMappingProfile()
    {
        CreateMap<ProductDTO, ProductCreateCommand>();
        CreateMap<ProductDTO, ProductUpdateCommand>();
    }
}
