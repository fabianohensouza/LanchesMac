using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LanchesMac.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o login do usuário")]
        [Display(Name = "Usuário")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
