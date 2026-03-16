namespace ToDo.ViewModels
{
    public class UtilizadorViewModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int TotalTarefas { get; set; }
        public int TarefasConcluidas { get; set; }
        public int TarefasPendentes { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime UltimoLogin { get; set; }
        public int DiasSemLogin { get; set; }
        public string Role { get; set; }
    }
}
