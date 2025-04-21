using System.ComponentModel.DataAnnotations;

namespace EducaOnline.Api.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "E-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "E-mail inválido")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Senha é obrigatório")]
    public string Senha { get; set; }
}