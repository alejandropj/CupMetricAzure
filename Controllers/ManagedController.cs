using CupMetric.Filters;
using CupMetric.Models;
using CupMetric.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using System.Security.Claims;

namespace CupMetric.Controllers
{
    public class ManagedController : Controller
    {
        private RepositoryUsers repo;
        public ManagedController(RepositoryUsers repo)
        {
            this.repo = repo;
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> LoginAsync(string email, string password)
        {
            User usuario = await this.repo.LoginUserAsync(email, password);
            if (usuario != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimTypes.Name, ClaimTypes.Role);

                Claim claimName = new Claim(ClaimTypes.Name, usuario.Nombre);
                identity.AddClaim(claimName);
                Claim claimId = new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString());
                identity.AddClaim(claimId);
                Claim claimRole = new Claim(ClaimTypes.Role, usuario.IdRol.ToString());
                identity.AddClaim(claimRole);
                if (usuario.IdRol == 2)
                {
                    //CREAMOS SU PROPIO Y UNICO CLAIM
                    identity.AddClaim
                        (new Claim("Administrador", "Admin"));
                }

                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);

                string controller = TempData["controller"].ToString();
                string action = TempData["action"].ToString();
                return RedirectToAction(action, controller);
            }
            else
            {
                ViewData["MENSAJE"] = "Email/Contraseña incorrectos";
                return View();
            }
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ErrorAcceso()
        {
            return View();
        }
        [AuthorizeUsuarios]
        public async Task<IActionResult> Personal()
        {
            int idUser = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            User user = await this.repo.FindUserByIdAsync(idUser);
            return View(user);
        }
    }
}
