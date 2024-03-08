using CupMetric.Models;
using CupMetric.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CupMetric.Controllers
{
    public class RecetasController : Controller
    {
        private RepositoryReceta repo;
        private RepositoryIngredientes repoIngredientes;
        public RecetasController(RepositoryReceta repo, RepositoryIngredientes repoIngredientes)
        {
            this.repo = repo;
            this.repoIngredientes = repoIngredientes;
        }
        public async Task<IActionResult> Index()
        {
            List<Receta> recetas = await this.repo.GetRecetasAsync();
            return View(recetas);
        }
        public async Task<IActionResult> Receta(int idreceta)
        {
            Receta receta = await this.repo.FindRecetaByIdAsync(idreceta);
            return View(receta);
        }
        [HttpPost]
        public async Task<IActionResult> Receta(int idreceta, int valoracion)
        {
            //Post receta await this.repo.GetRecetasAsync();
            ViewData["MENSAJE"] = "Gracias por tu valoración "+valoracion;
            Receta receta = await this.repo.FindRecetaByIdAsync(idreceta);
            return View(receta);
        }
        public async Task<IActionResult> List()
        {
            List<Receta> recetas = await this.repo.GetRecetasAsync();
            return View(recetas);
        }
        public async Task<IActionResult> Details(int idReceta)
        {
            Receta receta = await this.repo.FindRecetaByIdAsync(idReceta);
            return View(receta);
        }
        public async Task<IActionResult> Create()
        {
            List<Categoria> categorias = await this.repo.GetCategoriasAsync();
            List<Ingrediente> ingredientes = await this.repoIngredientes.GetIngredientesAsync();
            ViewData["CATEGORIAS"] = categorias;
            ViewData["INGREDIENTES"] = ingredientes;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Receta receta)
        {
            await this.repo.CreateRecetaAsync(receta);
            return RedirectToAction("List");
        }
        public async Task<IActionResult> Update(int idReceta)
        {
            Receta receta = await this.repo.FindRecetaByIdAsync((int)idReceta);
            return View(receta);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Receta receta)
        {
            await this.repo.UpdateRecetaAsync(receta);
            return RedirectToAction("List");
        }
        public async Task<IActionResult> Delete(int idReceta)
        {
            await this.repo.DeleteRecetaAsync(idReceta);
            return RedirectToAction("List");
        }
    }
}
