using CupMetric.Filters;
using CupMetric.Models;
using CupMetric.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CupMetric.Controllers
{
    public class UtensilioController : Controller
    {
        private RepositoryUtensilios repo;
        public UtensilioController(RepositoryUtensilios repo)
        {
            this.repo = repo;
        }
        [AuthorizeUsuarios(Policy = "AdminOnly")]
        public async Task<IActionResult> List()
        {
            List<Utensilio> utensilios = await this.repo.GetUtensiliosAsync();
            return View(utensilios);
        }
        [AuthorizeUsuarios(Policy = "AdminOnly")]
        public async Task<IActionResult> Details(int idUtensilio)
        {
            Utensilio utensilio = await this.repo.FindUtensilioByIdAsync(idUtensilio);
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
            await this.repo.CreateUtensilioAsync(utensilio);
            return RedirectToAction("List");
        }
        [AuthorizeUsuarios(Policy = "AdminOnly")]
        public async Task<IActionResult> Update(int idUtensilio)
        {
            Utensilio utensilio = await this.repo.FindUtensilioByIdAsync(idUtensilio);
            return View(utensilio);
        }
        [HttpPost]
        [AuthorizeUsuarios(Policy = "AdminOnly")]
        public async Task<IActionResult> Update(Utensilio utensilio)
        {
            await this.repo.UpdateUtensilioAsync(utensilio);
            return RedirectToAction("List");
        }
        [AuthorizeUsuarios(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(int idUtensilio)
        {
            await this.repo.DeleteUtensilioAsync(idUtensilio);
            return RedirectToAction("List");
        }
    }
}
