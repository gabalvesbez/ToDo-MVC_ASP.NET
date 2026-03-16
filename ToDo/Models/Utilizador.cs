using Microsoft.AspNetCore.Identity;

namespace ToDo.Models
{
    public class Utilizador : IdentityUser
    {
        public string PrimeiroNome { get; set; }
        public string Apelido { get; set; }
        public int UtilizadorAdmin {  get; set; }
        public DateTime UltimoLogin { get; set; } = DateTime.Now;
        public DateTime DataCriacao { get; set; } = DateTime.Now;

    }
}
