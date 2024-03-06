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
        public async Task<ActionResult> Index()
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
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: IngredienteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IngredienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: IngredienteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: IngredienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
