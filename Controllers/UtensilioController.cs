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
            List<Utensilio> utensilios = await this.repo.GetUtensiliosAsync();
            return View(utensilios);
        }        
        public async Task<IActionResult> Details(int idUtensilio)
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
            Utensilio utensilio = await this.repo.FindUtensilioByIdAsync(idUtensilio);
            return View(utensilio);
        }
        public async Task<IActionResult> Create()
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
        public async Task<IActionResult> Create(Utensilio utensilio)
        {
            await this.repo.CreateUtensilioAsync(utensilio);
            return RedirectToAction("List");
        }        
        public async Task<IActionResult> Update(int idUtensilio)
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
            Utensilio utensilio = await this.repo.FindUtensilioByIdAsync(idUtensilio);
            return View(utensilio);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Utensilio utensilio)
        {
            await this.repo.UpdateUtensilioAsync(utensilio);
            return RedirectToAction("List");
        }
        public async Task<IActionResult> Delete(int idUtensilio)
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
            await this.repo.DeleteUtensilioAsync(idUtensilio);
            return RedirectToAction("List");
        }
    }
}
