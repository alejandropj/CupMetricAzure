using CupMetric.Models;
using CupMetric.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CupMetric.Controllers
{
    public class ConverterController : Controller
    {
        private RepositoryIngredientes repoIngredientes { get; set; }
        public ConverterController(RepositoryIngredientes repositoryIngredientes) {
            this.repoIngredientes = repositoryIngredientes;
        }
        public async Task<IActionResult> Index()
        {
            List<Ingrediente> ingredientes = await this.repoIngredientes.GetIngredientesAsync();
            ViewData["INGREDIENTES"] = ingredientes;
            return View();
        }
    }
}
