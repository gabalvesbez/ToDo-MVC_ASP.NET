using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.Models;

namespace ToDo.Controllers
{
    [Authorize]
    public class HistoricoController : Controller
    {
        private readonly ToDoContext _context;
        private readonly UserManager<Utilizador> _userManager;

        public HistoricoController(ToDoContext context, UserManager<Utilizador> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Historico
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var tarefas = await _context.Tarefa
                .Include(t => t.Categoria)
                .Where(t => t.UtilizadorId == userId && t.Estado == "Concluída")
                .ToListAsync();
            return View(tarefas);
        }

        // POST: Historico/MarcarPendente/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarcarPendente(int id)
        {
            var userId = _userManager.GetUserId(User);
            var tarefa = await _context.Tarefa.FindAsync(id);
            if (tarefa == null || tarefa.UtilizadorId != userId)
            {
                return NotFound();
            }

            if (tarefa.Estado == "Concluída")
            {
                tarefa.Estado = "Pendente";
                _context.Update(tarefa);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Historico/Delete/5
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

        // POST: Historico/Delete/5
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
    }
}
