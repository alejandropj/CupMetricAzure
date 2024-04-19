using CupMetric.Filters;
using CupMetric.Models;
using CupMetric.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CupMetric.Controllers
{
    public class IngredienteController : Controller
    {
        private ServiceApiCupmetric service;
        public IngredienteController(ServiceApiCupmetric service)
        {
            this.service = service;
        }
        [AuthorizeUsuarios(Policy = "AdminOnly")]
        public async Task<ActionResult> List()
        {
            List<Ingrediente> ingredientes = await this.service.GetIngredientesAsync();
            return View(ingredientes);
        }
        [AuthorizeUsuarios(Policy = "AdminOnly")]
        public async Task<ActionResult> Details(int idIngrediente)
        {
            Ingrediente ingrediente = await this.service.FindIngredienteByIdAsync(idIngrediente);
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
                await this.service.CreateIngredienteAsync(ingrediente);
                return RedirectToAction("List");
        }
        [AuthorizeUsuarios(Policy = "AdminOnly")]
        public async Task<ActionResult> Update(int idingrediente)
        {
            Ingrediente ingrediente = await this.service.FindIngredienteByIdAsync(idingrediente);
            return View(ingrediente);
        }

        [AuthorizeUsuarios(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<ActionResult> Update(Ingrediente ingrediente)
        {
            await this.service.UpdateIngredienteAsync(ingrediente);
                return RedirectToAction("List");
        }
        [AuthorizeUsuarios(Policy = "AdminOnly")]
        public async Task<ActionResult> Delete(int idingrediente)
        {
            await this.service.DeleteIngredienteAsync(idingrediente);
            return RedirectToAction("List");
        }
    }
}
