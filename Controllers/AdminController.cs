using CupMetric.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CupMetric.Controllers
{
    public class AdminController : Controller
    {
        private RepositoryUsers repositoryUsers { get; set; }
        private RepositoryReceta repositoryRecetas { get; set; }
        private RepositoryUtensilios repositoryUtensilios { get; set; }
        private RepositoryIngredientes repositoryIngredientes { get; set; }
        public AdminController
            (RepositoryUsers repoUsers, RepositoryReceta repoReceta, RepositoryUtensilios repoUtensilios, RepositoryIngredientes repoIngredientes)
        {
            this.repositoryUsers = repoUsers;
            this.repositoryRecetas = repoReceta;
            this.repositoryUtensilios = repoUtensilios;
            this.repositoryIngredientes = repoIngredientes;
        }
        public async Task<IActionResult> Index()
        {
            int conteo = await this.repositoryUsers.CountUsersAsync();
            ViewData["CONTEOUSERS"] = conteo;
/*            int conteo = await this.repositoryRecetas.CountRecetasAsync();
            ViewData["CONTEORECETAS"] = conteo;            
            int conteo = await this.repositoryUtensilios.CountUtensiliosAsync();
            ViewData["CONTEOUSERS"] = conteo;            
            int conteo = await this.repositoryIngredientes.CountIngredientsAsync();
            ViewData["CONTEOUSERS"] = conteo;*/
            return View();
        }
    }
}
