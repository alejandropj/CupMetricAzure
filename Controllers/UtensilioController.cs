using CupMetric.Filters;
using CupMetric.Models;
using CupMetric.Services;
using Microsoft.AspNetCore.Mvc;

namespace CupMetric.Controllers
{
    public class UtensilioController : Controller
    {
        private ServiceApiCupmetric service;
        public UtensilioController(ServiceApiCupmetric service)
        {
            this.service = service;
        }
        [AuthorizeUsuarios(Policy = "AdminOnly")]
        public async Task<IActionResult> List()
        {
            List<Utensilio> utensilios = await this.service.GetUtensiliosAsync();
            return View(utensilios);
        }
        [AuthorizeUsuarios(Policy = "AdminOnly")]
        public async Task<IActionResult> Details(int idUtensilio)
        {
            Utensilio utensilio = await this.service.FindUtensilioByIdAsync(idUtensilio);
            return View(utensilio);
        }
        [AuthorizeUsuarios(Policy = "AdminOnly")]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [AuthorizeUsuarios(Policy = "AdminOnly")]
        public async Task<IActionResult> Create(Utensilio utensilio)
        {
            await this.service.CreateUtensilioAsync(utensilio);
            return RedirectToAction("List");
        }
        [AuthorizeUsuarios(Policy = "AdminOnly")]
        public async Task<IActionResult> Update(int idUtensilio)
        {
            Utensilio utensilio = await this.service.FindUtensilioByIdAsync(idUtensilio);
            return View(utensilio);
        }
        [HttpPost]
        [AuthorizeUsuarios(Policy = "AdminOnly")]
        public async Task<IActionResult> Update(Utensilio utensilio)
        {
            await this.service.UpdateUtensilioAsync(utensilio);
            return RedirectToAction("List");
        }
        [AuthorizeUsuarios(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(int idUtensilio)
        {
            await this.service.DeleteUtensilioAsync(idUtensilio);
            return RedirectToAction("List");
        }
    }
}
