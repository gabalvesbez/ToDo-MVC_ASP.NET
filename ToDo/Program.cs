using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using ToDo.Data;
using ToDo.Models;

namespace ToDo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Constrututor para o banco de dados:
            builder.Services.AddDbContext<ToDoContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoContext") ?? throw new InvalidOperationException("Connection string 'ToDoContext' not found.")));

            builder.Services.AddIdentity<Utilizador, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ToDoContext>()
                .AddDefaultTokenProviders();

            // Adiciona o serviço de autorização para as roles:
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
                options.AddPolicy("RequireGestorRole", policy => policy.RequireRole("Gestor"));
                options.AddPolicy("RequireClienteRole", policy => policy.RequireRole("Cliente"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Criar_Tarefa}/{id?}");

            app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Perfil}/{id?}");

            // Criação de roles:
            using (var scope = app.Services.CreateScope())
            {
                var roleManager =
                    scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new[] { "Admin", "Gestor", "Cliente" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                        await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Criação do utilizador Admin:
            using (var scope = app.Services.CreateScope())
            {
                var userManager =
                    scope.ServiceProvider.GetRequiredService<UserManager<Utilizador>>();

                // Credenciais do utilizador Admin:
                string primeiroNome = "Admin";
                string apelido = "Geral";
                string email = "admin@geral.com";
                string password = "admin12345";
                string dataCriacao = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                string ultimoLogin = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                // Verifica se o utilizador Admin já existe:
                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var utilizador = new Utilizador();
                    utilizador.PrimeiroNome = primeiroNome;
                    utilizador.Apelido = apelido;
                    utilizador.UserName = email;
                    utilizador.Email = email;
                    utilizador.DataCriacao = DateTime.Parse(dataCriacao);
                    utilizador.UltimoLogin = DateTime.Parse(ultimoLogin);

                    // Cria o utilizador e o adiciona à role Admin:
                    await userManager.CreateAsync(utilizador, password);
                    await userManager.AddToRoleAsync(utilizador, "Admin");

                    // Adiciona claims ao utilizador:
                    await userManager.AddClaimAsync(utilizador, new Claim("PrimeiroNome", primeiroNome));
                    await userManager.AddClaimAsync(utilizador, new Claim("Apelido", apelido));
                    await userManager.AddClaimAsync(utilizador, new Claim("Email", email));
                }
            }

            app.Run();

        }
    }
}