namespace ToDo.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string? UtilizadorId { get; set; }
        public string? Nome { get; set; }
        public string? Prioridade { get; set; }
        public string? Estado { get; set; }
        public int? CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
        public DateTime? DataCriacao { get; set; }
        public DateTime? DataLimite { get; set; }
    }
}
