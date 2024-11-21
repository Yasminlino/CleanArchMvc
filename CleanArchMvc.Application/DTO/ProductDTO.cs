using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Application.DTOs;

public class ProductDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The Name is Required")] // Obrigatório
    [MinLength(3)] //Minimo de caracters
    [MaxLength(100)] //Maximo de caracters
    [DisplayName("Name")]  
    public string? Name { get; set; }

    [Required(ErrorMessage = "The Description is Required")] // Obrigatório
    [MinLength(5)] //Minimo de caracters
    [MaxLength(200)] //Maximo de caracters
    [DisplayName("Description")]  
    public string? Description { get; set; }

    [Required(ErrorMessage = "The Price is Required")] // Obrigatório
    [Column(TypeName = "decimal(18,2)")]
    [DisplayFormat(DataFormatString = "{0:C2}")]
    [DataType(DataType.Currency)]
    [DisplayName("Price")] 
    public double Price { get; set; }

    [Required(ErrorMessage = "The Stock is Required")] // Obrigatório
    [Range(1, 9999)]
    [DisplayName("Stock")]
    public int Stock { get; set; }

    [MaxLength(250)]
    [DisplayName("Image")]
    public string? Image { get; set; }

    [JsonIgnore]
    public Category? Category { get; set; } 

    [DisplayName("Categories")]
    public int CategoryId { get; set; } 
}
