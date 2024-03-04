using CupMetric.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CupMetric.Controllers
{
    public class AdminController : Controller
    {
        private RepositoryUsers repositoryUsers { get; set; }
        public AdminController(RepositoryUsers repo)
        {
            this.repositoryUsers = repo;
        }
        public async Task<IActionResult> Index()
        {
            int conteo = await this.repositoryUsers.CountUsers();
            ViewData["CONTEOUSERS"] = conteo;
            return View();
        }
    }
}
