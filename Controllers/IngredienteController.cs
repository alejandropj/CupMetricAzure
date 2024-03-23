using CupMetric.Filters;
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
        [AuthorizeUsuarios(Policy = "AdminOnly")]
        public async Task<ActionResult> List()
        {
            List<Ingrediente> ingredientes = await this.repo.GetIngredientesAsync();
            return View(ingredientes);
        }
        [AuthorizeUsuarios(Policy = "AdminOnly")]
        public async Task<ActionResult> Details(int idIngrediente)
        {
            Ingrediente ingrediente = await this.repo.FindIngredienteByIdAsync(idIngrediente);
            return View(ingrediente);
        }
        [AuthorizeUsuarios(Policy = "AdminOnly")]
        public ActionResult Create()
        {
            return View();
        }
        [AuthorizeUsuarios(Policy = "AdminOnly")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Ingrediente ingrediente)
        {
                await this.repo.CreateIngredienteAsync(ingrediente);
                return RedirectToAction("List");
        }
        [AuthorizeUsuarios(Policy = "AdminOnly")]
        public async Task<ActionResult> Update(int idingrediente)
        {
            Ingrediente ingrediente = await this.repo.FindIngredienteByIdAsync(idingrediente);
            return View(ingrediente);
        }

        [AuthorizeUsuarios(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<ActionResult> Update(Ingrediente ingrediente)
        {
            await this.repo.UpdateIngredienteAsync(ingrediente);
                return RedirectToAction("List");
        }
        [AuthorizeUsuarios(Policy = "AdminOnly")]
        public async Task<ActionResult> Delete(int idingrediente)
        {
            await this.repo.DeleteIngredienteAsync(idingrediente);
            return RedirectToAction("List");
        }
    }
}
