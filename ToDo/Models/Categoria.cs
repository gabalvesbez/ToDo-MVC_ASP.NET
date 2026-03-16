namespace ToDo.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? UtilizadorId { get; set; }
        public Utilizador? Utilizador { get; set; }
    }
}
