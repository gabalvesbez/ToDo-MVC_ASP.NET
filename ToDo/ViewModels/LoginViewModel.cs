using System.ComponentModel.DataAnnotations;

namespace ToDo.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "A password é obrigatória.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Lembrar-me")]
        public bool LembrarMe { get; set; }
    }
}
