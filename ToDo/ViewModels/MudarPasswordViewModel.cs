using System.ComponentModel.DataAnnotations;

namespace ToDo.ViewModels
{
    public class MudarPasswordViewModel
    {
        [Required(ErrorMessage = "O Email é obrigatório.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "A Password é obrigatória.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "A {0} deve ter pelo menos {2} e no máximo {1} caracteres de comprimento.")]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Password")]
        public string NovaPassword { get; set; }

        [Required(ErrorMessage = "É necessário confirmar a Password.")]
        [Compare("NovaPassword", ErrorMessage = "A Password não coincide.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar nova Password")]
        public string ConfirmarNovaPassword { get; set; }
    }
}
