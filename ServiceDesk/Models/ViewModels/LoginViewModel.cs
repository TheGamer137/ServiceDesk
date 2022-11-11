using System.ComponentModel.DataAnnotations;

namespace ServiceDesk.Models.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Не указан Email")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email")]
    public string Email { get; set; }
         
    [Required(ErrorMessage = "Не указан пароль")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }
}