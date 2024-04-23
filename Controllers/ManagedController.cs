using CupMetric.Filters;
using CupMetric.Models;
using CupMetric.Services;
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
        private ServiceApiCupmetric service;
        public ManagedController(ServiceApiCupmetric service)
        {
            this.service = service;
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> LoginAsync(string email, string password)
        {
            TokenModel model = await this.service.LoginUserAsync(email, password);
            if (model != null)
            {
                HttpContext.Session.SetString("TOKEN", model.response);

                ClaimsIdentity identity = new ClaimsIdentity(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimTypes.Name, ClaimTypes.Role);

                Claim claimName = new Claim(ClaimTypes.Name, model.user.Nombre);
                identity.AddClaim(claimName);
                Claim claimId = new Claim(ClaimTypes.NameIdentifier, model.user.IdUsuario.ToString());
                identity.AddClaim(claimId);
                Claim claimRole = new Claim(ClaimTypes.Role, model.user.IdRol.ToString());
                identity.AddClaim(claimRole);
                if (model.user.IdRol == 2)
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
            User user = await this.service.FindUserByIdAsync(idUser);
            return View(user);
        }
    }
}
