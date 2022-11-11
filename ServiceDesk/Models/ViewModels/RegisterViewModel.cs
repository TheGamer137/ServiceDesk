using System.ComponentModel.DataAnnotations;

namespace ServiceDesk.Models.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Не указан Email")]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email")]
    public string Email { get; set; }
 
    [Required(ErrorMessage = "Введите имя")]
    [DataType(DataType.Text)]
    [Display(Name = "Имя")]
    public string FirstName { get; set; }
    
    [Required(ErrorMessage = "Введите фамилию")]
    [DataType(DataType.Text)]
    [Display(Name = "Фамилия")]
    public string LastName { get; set; }
 
    [Required(ErrorMessage = "Не указан Email")]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }
    
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [DataType(DataType.Password)]
    [Display(Name = "Подтвердить пароль")]
    public string PasswordConfirm { get; set; }
}