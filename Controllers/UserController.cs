using CupMetric.Filters;
using CupMetric.Models;
using CupMetric.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CupMetric.Controllers
{
    public class UserController : Controller
    {
        private ServiceApiCupmetric service;
        public UserController(ServiceApiCupmetric service)
        {
            this.service = service;
        }
        //REGISTER
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(string nombre, string email, string password)
        {
            await this.service.RegisterUserAsync(nombre, email, password);
            ViewData["MENSAJE"] = "Usuario registrado correctamente. Por favor inicie sesión";
            return View();
        }
        [AuthorizeUsuarios(Policy = "AdminOnly")]
        public async Task<IActionResult> List()
        {
            List<User> usuarios = await this.service.GetUsersAsync();
            return View(usuarios);
        }
        [AuthorizeUsuarios(Policy = "AdminOnly")]
        public async Task<IActionResult> Details(int IdUsuario)
        {
            User usuario = await this.service.FindUserByIdAsync(IdUsuario);
            return View(usuario);
        }
        [AuthorizeUsuarios(Policy = "AdminOnly")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AuthorizeUsuarios(Policy = "AdminOnly")]
        public async Task<IActionResult> Create(string nombre, string email, string password)
        {
            await this.service.RegisterUserAsync(nombre, email, password);
            return RedirectToAction("List");
        }
        [AuthorizeUsuarios]
        public async Task<IActionResult> Update(int IdUsuario)
        {
            User user = await this.service.FindUserByIdAsync((int)IdUsuario);
            user.Password = null;
            return View(user);
        }
        [HttpPost]
        [AuthorizeUsuarios]
        public async Task<IActionResult> Update(int idUsuario, string nombre, string email, string password)
        {
            await this.service.UpdateUserAsync(idUsuario, nombre, email,password);
            int idRol = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Role));
            if (idRol == 1)
            {
                return RedirectToAction("Personal","Managed");
            }
            else
            {
                return RedirectToAction("List");
            }
        }
        [AuthorizeUsuarios(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(int IdUsuario)
        {
            await this.service.DeleteUserAsync(IdUsuario);
            return RedirectToAction("List");
        }
    }
}
