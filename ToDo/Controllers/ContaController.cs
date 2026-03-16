using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDo.Models;
using ToDo.ViewModels;
using System.Security.Claims;

namespace ToDo.Controllers
{
    public class ContaController : Controller
    {
        private readonly SignInManager<Utilizador> signInManager;
        private readonly UserManager<Utilizador> userManager;

        public ContaController(SignInManager<Utilizador> signInManager, UserManager<Utilizador> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var resultado = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.LembrarMe, false);

                if (resultado.Succeeded)
                {
                    var utilizador = await userManager.FindByEmailAsync(model.Email);
                    if (utilizador != null)
                    {
                        utilizador.UltimoLogin = DateTime.Now;
                        await userManager.UpdateAsync(utilizador);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Email e/ou password incorreto(s).");
                    return View(model);
                }
            }
            return View(model);
        }

        public IActionResult Registar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registar(RegistoViewModel model)
        {
            if (ModelState.IsValid)
            {
                Utilizador utilizador = new Utilizador
                {
                    PrimeiroNome = model.PrimeiroNome,
                    Apelido = model.Apelido,
                    Email = model.Email,
                    UserName = model.Email,
                    DataCriacao = DateTime.Now,
                    UltimoLogin = DateTime.Now
                };

                var resultado = await userManager.CreateAsync(utilizador, model.Password);

                if (resultado.Succeeded)
                {
                    await userManager.AddClaimAsync(utilizador, new Claim("PrimeiroNome", model.PrimeiroNome));
                    await userManager.AddClaimAsync(utilizador, new Claim("Apelido", model.Apelido));
                    await userManager.AddClaimAsync(utilizador, new Claim("Email", model.Email));

                    // Adiciona a role de Cliente ao novo utilizador:
                    await userManager.AddToRoleAsync(utilizador, "Cliente");

                    return RedirectToAction("Login", "Conta");
                }
                else
                {
                    foreach (var error in resultado.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(model);
                }
            }
            return View(model);
        }

        public IActionResult VerificarEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerificarEmail(VerificarEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                var utilizador = await userManager.FindByNameAsync(model.Email);

                if (utilizador == null)
                {
                    ModelState.AddModelError("", "Email não encontrado!");
                    return View(model);
                }
                else
                {
                    return RedirectToAction("MudarPassword", "Conta", new { username = utilizador.UserName });
                }
            }
            return View(model);
        }

        public IActionResult MudarPassword(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("VerificarEmail", "Conta");
            }
            return View(new MudarPasswordViewModel { Email = username });
        }

        [HttpPost]
        public async Task<IActionResult> MudarPassword(MudarPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var utilizador = await userManager.FindByNameAsync(model.Email);
                if (utilizador != null)
                {
                    var resultado = await userManager.RemovePasswordAsync(utilizador);
                    if (resultado.Succeeded)
                    {
                        resultado = await userManager.AddPasswordAsync(utilizador, model.NovaPassword);
                        return RedirectToAction("Login", "Conta");
                    }
                    else
                    {
                        foreach (var error in resultado.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Email não encontrado!");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Algo deu errado, tente novamente!");
                return View(model);
            }
        }

        public async Task<IActionResult> Sair()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
