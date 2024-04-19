using CupMetric.Filters;
using CupMetric.Services;
using Microsoft.AspNetCore.Mvc;

namespace CupMetric.Controllers
{
    public class AdminController : Controller
    {
        private ServiceApiCupmetric service;
        public AdminController
            (ServiceApiCupmetric service)
        {
            this.service = service;
        }
        [AuthorizeUsuarios(Policy = "AdminOnly")]
        public async Task<IActionResult> Index()
        {
            int conteoUsers = await this.service.CountUsersAsync();
            ViewData["CONTEOUSERS"] = conteoUsers;
            int conteoRecetas = await this.service.CountRecetasAsync();
            ViewData["CONTEORECETAS"] = conteoRecetas;
            int conteoUtensilios = await this.service.CountUtensiliosAsync();
            ViewData["CONTEOUTENSILIOS"] = conteoUtensilios;
            int conteoIngredientes = await this.service.CountIngredientesAsync();
            ViewData["CONTEOINGREDIENTES"] = conteoIngredientes;
            return View();
        }
    }
}
