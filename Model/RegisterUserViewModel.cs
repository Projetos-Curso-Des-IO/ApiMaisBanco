using System.ComponentModel.DataAnnotations;

namespace ApiFuncional.Model
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email não está em um formato válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")] 
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")] 
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmPassword { get; set; }
    }


    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email não está em um formato válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres.")]
        public string Password { get; set; }
    }

}
