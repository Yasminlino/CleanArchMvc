using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CleanArchMvc.Application.DTOs;

public class CategoryDTO
{
    public int Id { get; set;}

    [Required(ErrorMessage = "The Name is Required")] // Obrigatório
    [MinLength(3)] //Minimo de caracters
    [MaxLength(100)] //Maximo de caracters
    [DisplayName("Name")]    
    public string? Name { get; private set; }
}
