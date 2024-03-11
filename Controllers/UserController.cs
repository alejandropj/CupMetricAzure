using CupMetric.Models;
using CupMetric.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CupMetric.Controllers
{
    public class UserController : Controller
    {
        private RepositoryUsers repo;
        public UserController(RepositoryUsers repo)
        {
            this.repo = repo;
        }
        // LOGIN
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            User user = await this.repo.LoginUserAsync(email, password);
            if (user == null)
            {
                ViewData["MENSAJE"] = "Credenciales incorrectas";
                return View();
            }
            else
            {
                HttpContext.Session.SetString("USUARIO", user.Nombre);
                HttpContext.Session.SetString("IDROL", user.IdRol.ToString());
                HttpContext.Session.SetString("IDUSUARIO", user.IdUsuario.ToString());
                ViewData["MENSAJE"] = "Usuario validado";
            }
            return RedirectToAction("Index","Home");
        }
        //REGISTER
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(string nombre, string email, string password)
        {
            await this.repo.RegisterUserAsync(nombre, email, password);
            ViewData["MENSAJE"] = "Usuario registrado correctamente. Por favor inicie sesión";
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("USUARIO");
            HttpContext.Session.Remove("IDROL");
            HttpContext.Session.Remove("IDUSUARIO");
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Personal()
        {
            int idUsuario = int.Parse(HttpContext.Session.GetString("IDUSUARIO"));
            User user = await this.repo.FindUserByIdAsync(idUsuario);
            return View(user);
        }

        //START OF USER
        public async Task<IActionResult> List()
        {
            List<User> usuarios = await this.repo.GetUsersAsync();
            return View(usuarios);
        }
        public async Task<IActionResult> Details(int IdUsuario)
        {
            User usuario = await this.repo.FindUserByIdAsync(IdUsuario);
            return View(usuario);
        }        
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string nombre, string email, string password)
        {
            await this.repo.RegisterUserAsync(nombre, email, password);
            return RedirectToAction("List");
        }        
        public async Task<IActionResult> Update(int IdUsuario)
        {
            User user = await this.repo.FindUserByIdAsync((int)IdUsuario);
            user.Password = null;
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int idUsuario, string nombre, string email, string password)
        {
            await this.repo.UpdateUserAsync(idUsuario, nombre, email,password);
            return RedirectToAction("List");
        }
        public async Task<IActionResult> Delete(int IdUsuario)
        {
            await this.repo.DeleteUserAsync(IdUsuario);
            return RedirectToAction("List");
        }
    }
}
