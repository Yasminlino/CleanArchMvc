using System.ComponentModel.DataAnnotations;


namespace CleanArchMvc.API.DTO;
public class RegisterDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "Password don't match")]
    public string ConfirmPassWord { get; set; }
}