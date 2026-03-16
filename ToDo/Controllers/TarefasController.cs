using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.Models;

namespace ToDo.Controllers
{
    [Authorize]
    public class TarefasController : Controller
    {
        private readonly ToDoContext _context;
        private readonly UserManager<Utilizador> _userManager;

        public TarefasController(ToDoContext context, UserManager<Utilizador> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private void PopulatePrioridadeDropDownList(object? selectedPrioridade = null)
        {
            var prioridades = new List<SelectListItem>
            {
                new SelectListItem { Value = "Ainda pode esperar", Text = "Ainda pode esperar" },
                new SelectListItem { Value = "Importante", Text = "Importante" },
                new SelectListItem { Value = "Muito Importante", Text = "Muito Importante" },
                new SelectListItem { Value = "Urgente", Text = "Urgente" }
            };
            ViewBag.Prioridades = new SelectList(prioridades, "Value", "Text", selectedPrioridade);
        }

        private void PopulateEstadoDropDownList(object? selectedEstado = null)
        {
            var estados = new List<SelectListItem>
            {
                new SelectListItem { Value = "Pendente", Text = "Pendente" },
                new SelectListItem { Value = "Concluída", Text = "Concluída" }
            };
            ViewBag.Estados = new SelectList(estados, "Value", "Text", selectedEstado);
        }

        private void PopulateCategoriasDropDownList(string userId, object? selectedCategoria = null)
        {
            var categorias = _context.Categoria
                .Where(c => c.UtilizadorId == userId)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Nome
                }).ToList();

            ViewBag.Categorias = new SelectList(categorias, "Value", "Text", selectedCategoria);
        }

        // GET: Tarefas
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var tarefas = await _context.Tarefa
                .Include(t => t.Categoria) // Inclui a categoria relacionada
                .Where(t => t.UtilizadorId == userId && t.Estado == "Pendente") // Filtra as tarefas pendentes do utilizador autenticado
                .ToListAsync();
            return View(tarefas);
        }

        // GET: Tarefas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var tarefa = await _context.Tarefa
                .Include(t => t.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id && m.UtilizadorId == userId);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

        // GET: Tarefas/Create
        public IActionResult Create()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }
            PopulateCategoriasDropDownList(userId);
            PopulatePrioridadeDropDownList();
            PopulateEstadoDropDownList();
            return View();
        }

        // POST: Tarefas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Prioridade,Estado,CategoriaId,DataCriacao,DataLimite")] Tarefa tarefa)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }
            tarefa.UtilizadorId = userId;

            if (ModelState.IsValid)
            {
                _context.Add(tarefa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            PopulateCategoriasDropDownList(userId, tarefa.CategoriaId);
            PopulatePrioridadeDropDownList(tarefa.Prioridade);
            PopulateEstadoDropDownList(tarefa.Estado);
            return View(tarefa);
        }

        // GET: Tarefas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var tarefa = await _context.Tarefa.FindAsync(id);
            if (tarefa == null || tarefa.UtilizadorId != userId)
            {
                return Unauthorized();
            }

            PopulateCategoriasDropDownList(userId!, tarefa.CategoriaId);
            PopulatePrioridadeDropDownList(tarefa.Prioridade);
            PopulateEstadoDropDownList(tarefa.Estado);
            return View(tarefa);
        }

        // POST: Tarefas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Prioridade,Estado,CategoriaId,DataCriacao,DataLimite")] Tarefa tarefa)
        {
            if (id != tarefa.Id)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var tarefaExistente = await _context.Tarefa.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
            if (tarefaExistente == null || tarefaExistente.UtilizadorId != userId)
            {
                return Unauthorized();
            }

            tarefa.UtilizadorId = userId;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarefa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TarefaExists(tarefa.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            PopulateCategoriasDropDownList(userId!, tarefa.CategoriaId);
            PopulatePrioridadeDropDownList(tarefa.Prioridade);
            PopulateEstadoDropDownList(tarefa.Estado);
            return View(tarefa);
        }

        // GET: Tarefas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var tarefa = await _context.Tarefa
                .Include(t => t.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id && m.UtilizadorId == userId);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

        // POST: Tarefas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = _userManager.GetUserId(User);
            var tarefa = await _context.Tarefa.FindAsync(id);
            if (tarefa == null || tarefa.UtilizadorId != userId)
            {
                return NotFound();
            }

            _context.Tarefa.Remove(tarefa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Tarefas/MarcarConcluida/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarcarConcluida(int id)
        {
            var userId = _userManager.GetUserId(User);
            var tarefa = await _context.Tarefa.FindAsync(id);
            if (tarefa == null || tarefa.UtilizadorId != userId)
            {
                return NotFound();
            }

            if (tarefa.Estado != "Concluída")
            {
                tarefa.Estado = "Concluída";
                _context.Update(tarefa);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TarefaExists(int id)
        {
            return _context.Tarefa.Any(e => e.Id == id);
        }
    }
}
