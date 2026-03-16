using System.ComponentModel.DataAnnotations;

namespace ToDo.ViewModels
{
    public class VerificarEmailViewModel
    {
        [Required(ErrorMessage = "O Email é obrigatório.")]
        [Display(Name = "Insira o endereço eletrônico associado à sua conta:")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
