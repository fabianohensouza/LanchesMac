using System.ComponentModel.DataAnnotations;

namespace LanchesMac.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Informe o login do usuário")]
        [Display(Name = "Usuário")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Informe a Senha")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Repita a senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Repita a Senha")]
        public string ConfirmPassword { get; set; }
        public string ReturnUrl { get; set; }
    }
}