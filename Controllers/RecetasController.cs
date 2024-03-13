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
            List<RecetaIngredienteValoracion> recetas = await this.repo.GetRecetasFormattedAsync();
            return View(recetas);
        }
        public async Task<IActionResult> Receta(int idreceta)
        {
            RecetaIngredienteValoracion receta = await this.repo.FindRecetaFormattedAsync(idreceta);
            await this.repo.AddVisitRecetaAsync(idreceta);
            return View(receta);
        }
        [HttpPost]
        public async Task<IActionResult> Receta(int idreceta, int valoracion)
        {
            int idUsuario = 0;
            bool exists = await this.repo.PostValoracionAsync(idreceta, idUsuario ,valoracion);
            if (exists)
            {
                ViewData["MENSAJE"] = "Ya has valorado esta receta";
            }
            ViewData["MENSAJE"] = "Gracias por tu valoración.";

            //Carga
            RecetaIngredienteValoracion receta = await this.repo.FindRecetaFormattedAsync(idreceta);
            return View(receta);
        }
        //Admin
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
        public async Task<IActionResult> Create(Receta receta, IFormCollection form)
        {
            List<int> idIngredientes = new List<int>();
            List<int> cantidades = new List<int>();
            foreach (var key in form.Keys)
            {
                if (key.StartsWith("dynamicInputId_"))
                {
                    var index = int.Parse(key.Substring("dynamicInputId_".Length));
                    idIngredientes.Add(int.Parse(form[key]));
                    cantidades.Add(int.Parse(form["dynamicInputCantidad_" + index]));
                }
            }
            //await this.repo.CreateRecetaAsync(receta);
            receta.Visitas = 0;

            Receta rec=new Receta();
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
