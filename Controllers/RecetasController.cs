using CupMetric.Models;
using CupMetric.Repositories;
using Microsoft.AspNetCore.Http;
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
            ViewData["SelectedFilter"] = 0;
            ViewData["CATEGORIAS"] = await this.repo.GetCategoriasAsync();
            return View(recetas);
        }
        [HttpPost]
        public async Task<IActionResult> Index(int filter)
        {
            List<RecetaIngredienteValoracion> recetas;
            if (filter == 0)
            {
                recetas = await this.repo.GetRecetasFormattedAsync();
            }
            else
            {
                recetas = await this.repo.FilterRecetaByCategoriaAsync(filter);
            }
            ViewData["SelectedFilter"] = filter;
            ViewData["CATEGORIAS"] = await this.repo.GetCategoriasAsync();
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
            int idUsuario = int.Parse(HttpContext.Session.GetString("IDUSUARIO"));
            bool exists = await this.repo.PostValoracionAsync(idreceta, idUsuario ,valoracion);
            if (exists)
            {
                ViewData["MENSAJE"] = "Ya has valorado esta receta";
            }
            else
            {
                ViewData["MENSAJE"] = "Gracias por tu valoración.";
            }

            //Carga
            RecetaIngredienteValoracion receta = await this.repo.FindRecetaFormattedAsync(idreceta);
            return View(receta);
        }
        //Admin
        public async Task<IActionResult> List()
        {
            string rol = HttpContext.Session.GetString("IDROL");
            if (rol == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int idRol = int.Parse(HttpContext.Session.GetString("IDROL"));
            if (idRol != 2)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Receta> recetas = await this.repo.GetRecetasAsync();
            return View(recetas);
        }
        public async Task<IActionResult> Details(int idReceta)
        {
            string rol = HttpContext.Session.GetString("IDROL");
            if (rol == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int idRol = int.Parse(HttpContext.Session.GetString("IDROL"));
            if (idRol != 2)
            {
                return RedirectToAction("Index", "Home");
            }
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
            List<double> cantidades = new List<double>();
            foreach (var key in form.Keys)
            {
                if (key.StartsWith("dynamicInputId_"))
                {
                    var index = int.Parse(key.Substring("dynamicInputId_".Length));
                    idIngredientes.Add(int.Parse(form[key]));
                    cantidades.Add(double.Parse(form["dynamicInputCantidad_" + index]));
                }
            }
            receta.Visitas = 0;

            await this.repo.CreateRecetaAsync(receta, idIngredientes, cantidades);
            return RedirectToAction("List");
        }
        public async Task<IActionResult> Update(int idReceta)
        {
            string rol = HttpContext.Session.GetString("IDROL");
            if (rol == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int idRol = int.Parse(HttpContext.Session.GetString("IDROL"));
            if (idRol != 2)
            {
                return RedirectToAction("Index", "Home");
            }
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
            string rol = HttpContext.Session.GetString("IDROL");
            if (rol == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int idRol = int.Parse(HttpContext.Session.GetString("IDROL"));
            if (idRol != 2)
            {
                return RedirectToAction("Index", "Home");
            }
            await this.repo.DeleteRecetaAsync(idReceta);
            return RedirectToAction("List");
        }
    }
}
