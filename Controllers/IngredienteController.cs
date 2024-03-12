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
        // GET: IngredienteController
        public async Task<ActionResult> List()
        {
            List<Ingrediente> ingredientes = await this.repo.GetIngredientesAsync();
            return View(ingredientes);
        }

        // GET: IngredienteController/Details/5
        public async Task<ActionResult> Details(int idIngrediente)
        {
            Ingrediente ingrediente = await this.repo.FindIngredienteByIdAsync(idIngrediente);
            return View(ingrediente);
        }

        // GET: IngredienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IngredienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Ingrediente ingrediente)
        {
            try
            {
                await this.repo.CreateIngredienteAsync(ingrediente);
                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> Update(int idingrediente)
        {
            Ingrediente ingrediente = await this.repo.FindIngredienteByIdAsync(idingrediente);
            return View(ingrediente);
        }

        // POST: IngredienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(Ingrediente ingrediente)
        {
            try
            {
                await this.repo.UpdateIngredienteAsync(ingrediente);
                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }

        // GET: IngredienteController/Delete/5
        public async Task<ActionResult> Delete(int idingrediente)
        {
            this.repo.DeleteIngredienteAsync(idingrediente);
            return View();
        }
    }
}
