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
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            return View();
        }        
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User usuario)
        {
            return View();
        }

        public async Task<IActionResult> ListUsuarios()
        {
            List<User> usuarios = await this.repo.GetUsers();
            return View(usuarios);
        }
        public async Task<IActionResult> DetailsUsuario(int IdUsuario)
        {
            User usuario = await this.repo.FindUserById(IdUsuario);
            return View(usuario);
        }
    }
}
