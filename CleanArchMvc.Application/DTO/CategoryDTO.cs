using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CleanArchMvc.Application.DTOs;

public class CategoryDTO
{
    public int Id { get; set;}

    [StringLength(100, MinimumLength = 3, ErrorMessage = "The Name must be between 3 and 100 characters.")]
    [Required(ErrorMessage = "The Name is Required")]
    public string Name { get; set; }

    // Construtor sem parâmetros (obrigatório para binding de modelos)
    public CategoryDTO() { }

    // Caso você precise de um construtor com parâmetros para facilitar a inicialização
    public CategoryDTO(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
