using System.ComponentModel.DataAnnotations;

namespace ToDo.ViewModels
{
    public class RegistoViewModel
    {
        [Required(ErrorMessage = "O Primeiro Nome é obrigatório.")]
        [Display(Name = "Primeiro nome")]
        public string PrimeiroNome { get; set; }

        [Required(ErrorMessage = "O Apelido é obrigatório.")]
        public string Apelido { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "A Password é obrigatória.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "A {0} deve ter pelo menos {2} e no máximo {1} caracteres de comprimento.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "É necessário confirmar a Password.")]
        [Compare("Password", ErrorMessage = "A Password não coincide.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Password")]
        public string ConfirmarPassword { get; set; }
    }
}
