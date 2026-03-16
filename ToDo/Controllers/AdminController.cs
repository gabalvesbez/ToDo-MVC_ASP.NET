using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.Models;
using ToDo.ViewModels;

namespace ToDo.Controllers
{
    [Authorize(Roles = "Admin,Gestor")]
    public class AdminController : Controller
    {
        private readonly ToDoContext _context;
        private readonly UserManager<Utilizador> _userManager;

        public AdminController(ToDoContext context, UserManager<Utilizador> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Dashboard()
        {
            var totalUtilizadores = _context.Utilizador.Count();
            var totalTarefas = _context.Tarefa.Count();
            var tarefasCompletadas = _context.Tarefa.Count(t => t.Estado == "Concluída");

            var model = new DashboardViewModel
            {
                TotalUtilizadores = totalUtilizadores,
                TotalTarefas = totalTarefas,
                TarefasCompletadas = tarefasCompletadas
            };

            return View(model);
        }

        public async Task<IActionResult> GerirUtilizadores(string sortOrder)
        {
            var utilizadores = await _context.Utilizador.ToListAsync();
            var adminEmail = "admin@geral.com";

            var utilizadorViewModels = new List<UtilizadorViewModel>();

            foreach (var utilizador in utilizadores)
            {
                if (utilizador.Email != adminEmail)
                {
                    var roles = await _userManager.GetRolesAsync(utilizador);
                    var role = roles.FirstOrDefault() ?? "Cliente";

                    utilizadorViewModels.Add(new UtilizadorViewModel
                    {
                        Id = utilizador.Id,
                        Nome = $"{utilizador.PrimeiroNome} {utilizador.Apelido}",
                        Email = utilizador.Email,
                        DataCriacao = utilizador.DataCriacao,
                        UltimoLogin = utilizador.UltimoLogin,
                        DiasSemLogin = (DateTime.Now - utilizador.UltimoLogin).Days,
                        Role = role
                    });
                }
            }

            // Ordenação:
            ViewBag.NomeSortParm = String.IsNullOrEmpty(sortOrder) ? "nome_desc" : "";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "email_desc" : "Email";
            ViewBag.DataCriacaoSortParm = sortOrder == "DataCriacao" ? "dataCriacao_desc" : "DataCriacao";
            ViewBag.UltimoLoginSortParm = sortOrder == "UltimoLogin" ? "ultimoLogin_desc" : "UltimoLogin";
            ViewBag.DiasSemLoginSortParm = sortOrder == "DiasSemLogin" ? "diasSemLogin_desc" : "DiasSemLogin";

            utilizadorViewModels = sortOrder switch
            {
                "nome_desc" => utilizadorViewModels.OrderByDescending(u => u.Nome).ToList(),
                "Email" => utilizadorViewModels.OrderBy(u => u.Email).ToList(),
                "email_desc" => utilizadorViewModels.OrderByDescending(u => u.Email).ToList(),
                "DataCriacao" => utilizadorViewModels.OrderBy(u => u.DataCriacao).ToList(),
                "dataCriacao_desc" => utilizadorViewModels.OrderByDescending(u => u.DataCriacao).ToList(),
                "UltimoLogin" => utilizadorViewModels.OrderBy(u => u.UltimoLogin).ToList(),
                "ultimoLogin_desc" => utilizadorViewModels.OrderByDescending(u => u.UltimoLogin).ToList(),
                "DiasSemLogin" => utilizadorViewModels.OrderBy(u => u.DiasSemLogin).ToList(),
                "diasSemLogin_desc" => utilizadorViewModels.OrderByDescending(u => u.DiasSemLogin).ToList(),
                _ => utilizadorViewModels.OrderBy(u => u.Nome).ToList(),
            };

            return View(utilizadorViewModels);
        }

        // GET: Admin/DeleteUser/5
        public async Task<IActionResult> DeleteUser(string? id)
        {
            // Verifica se o ID é nulo:
            if (id == null)
            {
                return NotFound();
            }

            // Procura o utilizador pelo ID:
            var utilizador = await _context.Utilizador
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utilizador == null)
            {
                return NotFound();
            }

            // Conta o total de tarefas, tarefas completadas e tarefas pendentes do utilizador:
            var totalTarefas = await _context.Tarefa.CountAsync(t => t.UtilizadorId == id);
            var tarefasCompletadas = await _context.Tarefa.CountAsync(t => t.UtilizadorId == id && t.Estado == "Concluída");
            var tarefasPendentes = totalTarefas - tarefasCompletadas;

            // Adiciona as variáveis à ViewBag:
            ViewBag.TotalTarefas = totalTarefas;
            ViewBag.TarefasCompletadas = tarefasCompletadas;
            ViewBag.TarefasPendentes = tarefasPendentes;

            return View(utilizador);
        }

        // POST: Admin/DeleteUser/5
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUserConfirmed(string id)
        {
            // Procura o utilizador pelo ID:
            var utilizador = await _userManager.FindByIdAsync(id);
            if (utilizador != null)
            {
                // Verifica se o utilizador é um Gestor e se o utilizador atual é Admin:
                var currentUser = await _userManager.GetUserAsync(User);
                var currentUserRoles = await _userManager.GetRolesAsync(currentUser);
                var currentUserIsAdmin = currentUserRoles.Contains("Admin");

                if (await _userManager.IsInRoleAsync(utilizador, "Gestor") && !currentUserIsAdmin)
                {
                    return Forbid();
                }

                // Exclui as tarefas associadas ao usuário:
                var tarefas = _context.Tarefa.Where(t => t.UtilizadorId == id);
                _context.Tarefa.RemoveRange(tarefas);

                // Exclui as categorias associadas ao usuário:
                var categorias = _context.Categoria.Where(c => c.UtilizadorId == id);
                _context.Categoria.RemoveRange(categorias);

                // Salva as alterações no banco de dados:
                await _context.SaveChangesAsync();

                // Exclui o usuário:
                var result = await _userManager.DeleteAsync(utilizador);
                if (result.Succeeded)
                {
                    // Se o resultado for bem sucedido, redireciona para a página de gerir utilizadores:
                    return RedirectToAction(nameof(GerirUtilizadores));
                }
                else
                {
                    // Adiciona os erros ao ModelState:
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return RedirectToAction(nameof(GerirUtilizadores));
        }

        // GET: Admin/PromoverParaGestor/5
        public async Task<IActionResult> PromoverParaGestor(string? id)
        {
            // Verifica se o ID é nulo:
            if (id == null)
            {
                return NotFound();
            }

            // Procura o utilizador pelo ID:
            var utilizador = await _context.Utilizador
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utilizador == null)
            {
                return NotFound();
            }

            // Retorna a view com o utilizador:
            return View(utilizador);
        }

        // POST: Admin/PromoverParaGestor/5
        [HttpPost, ActionName("PromoverParaGestor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PromoverParaGestorConfirmed(string id)
        {
            // Procura o utilizador pelo ID:
            var utilizador = await _userManager.FindByIdAsync(id);
            if (utilizador != null)
            {
                // Adiciona o utilizador à role Gestor:
                var removerRoleCliente = await _userManager.RemoveFromRoleAsync(utilizador, "Cliente");
                var result = await _userManager.AddToRoleAsync(utilizador, "Gestor");
                if (result.Succeeded)
                {
                    // Redireciona para a página de gerir utilizadores:
                    return RedirectToAction(nameof(GerirUtilizadores));
                }
                else
                {
                    // Adiciona os erros ao ModelState:
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            // Redireciona para a página de gerir utilizadores:
            return RedirectToAction(nameof(GerirUtilizadores));
        }

        // GET: Admin/RebaixarParaCliente/5
        public async Task<IActionResult> RebaixarParaCliente(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var utilizador = await _context.Utilizador
                .FirstOrDefaultAsync(m => m.Id == id);
            if (utilizador == null)
            {
                return NotFound();
            }

            return View(utilizador);
        }

        // POST: Admin/RebaixarParaCliente/5
        [HttpPost, ActionName("RebaixarParaCliente")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RebaixarParaClienteConfirmed(string id)
        {
            var utilizador = await _userManager.FindByIdAsync(id);
            if (utilizador != null)
            {
                var removerRoleGestor = await _userManager.RemoveFromRoleAsync(utilizador, "Gestor");
                var result = await _userManager.AddToRoleAsync(utilizador, "Cliente");
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(GerirUtilizadores));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return RedirectToAction(nameof(GerirUtilizadores));
        }
    }
}
