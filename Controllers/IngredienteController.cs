using CupMetric.Models;
using CupMetric.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CupMetric.Controllers
{
    public class IngredienteController : Controller
    {
        private RepositoryIngredientes repo;
        public IngredienteController(RepositoryIngredientes repo)
        {
            this.repo = repo;
        }
        public async Task<ActionResult> List()
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
            List<Ingrediente> ingredientes = await this.repo.GetIngredientesAsync();
            return View(ingredientes);
        }

        public async Task<ActionResult> Details(int idIngrediente)
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
            Ingrediente ingrediente = await this.repo.FindIngredienteByIdAsync(idIngrediente);
            return View(ingrediente);
        }

        public ActionResult Create()
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Ingrediente ingrediente)
        {
                await this.repo.CreateIngredienteAsync(ingrediente);
                return RedirectToAction("List");
        }

        public async Task<ActionResult> Update(int idingrediente)
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
            Ingrediente ingrediente = await this.repo.FindIngredienteByIdAsync(idingrediente);
            return View(ingrediente);
        }

        [HttpPost]
        public async Task<ActionResult> Update(Ingrediente ingrediente)
        {
            await this.repo.UpdateIngredienteAsync(ingrediente);
                return RedirectToAction("List");
        }
        public async Task<ActionResult> Delete(int idingrediente)
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
            await this.repo.DeleteIngredienteAsync(idingrediente);
            return RedirectToAction("List");
        }
    }
}
