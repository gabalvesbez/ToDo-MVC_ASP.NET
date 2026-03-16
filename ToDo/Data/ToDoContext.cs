using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDo.Models;

namespace ToDo.Data
{
    public class ToDoContext : IdentityDbContext
    {
        public ToDoContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ToDo.Models.Tarefa> Tarefa { get; set; } = default!;
        public DbSet<ToDo.Models.Categoria> Categoria { get; set; } = default!;
        public DbSet<ToDo.Models.Utilizador> Utilizador { get; set; } = default!;
    }
}
