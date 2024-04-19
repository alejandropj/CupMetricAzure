﻿using CupMetric.Models;
using CupMetric.Services;
using Microsoft.AspNetCore.Mvc;

namespace CupMetric.Controllers
{
    public class ConverterController : Controller
    {
        private ServiceApiCupmetric service;
        public ConverterController(ServiceApiCupmetric service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
            List<Ingrediente> ingredientes = await this.service.GetIngredientesMediblesAsync();
            ViewData["INGREDIENTES"] = ingredientes;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(int ingrediente, double cantidad, int unidad)
        {
            List<Ingrediente> ingredientes = await this.service.GetIngredientesMediblesAsync();
            ViewData["INGREDIENTES"] = ingredientes;
            List<Utensilio> utensilios = await this.service.GetUtensiliosAsync();
            Ingrediente ingr = await this.service.FindIngredienteByIdAsync(ingrediente);
            if (ingr.Densidad != null)
            {
                double densidad = ingr.Densidad.Value;
                double masa = 0;
                //Hay que convertir a mL
                //Conversor
                switch (unidad)
                {
                    case 1:
                        //gramos
                        masa = cantidad / densidad;

                    break;
                    case 2:
                        //mililitros
                        masa = cantidad;
                    break;
                    case 3:
                        //kilos
                        masa = cantidad * 1000 / densidad;
                    break;
                    case 4:
                        //litros
                        masa = cantidad * 1000;
                    break;
                    default:
                        masa = cantidad; 
                        break;
                }
                double volumen = densidad * masa;

                var utensiliosSeleccionados = new List<Utensilio>();
                var utensiliosOrdenados = utensilios.OrderByDescending(u => u.Volumen);

                foreach (var utensilio in utensiliosOrdenados)
                {
                    while (volumen >= utensilio.Volumen)
                    {
                        // Agrega el utensilio y actualiza el volumen restante
                        utensiliosSeleccionados.Add(utensilio);
                        volumen -= utensilio.Volumen;
                    }
                }
                    //Si aún queda volumen por cubrir
/*                    if (volumen > 0)
                {
                    foreach (var utensilio in utensiliosOrdenados)
                    {
                        if (!utensiliosSeleccionados.Contains(utensilio))
                        {
                            if (volumen <= utensilio.Volumen)
                            {
                                utensiliosSeleccionados.Add(utensilio);
                                break;
                            }
                        }
                    }
                }*/

                if(utensiliosSeleccionados.Count!= 0)
                {
                    ViewData["UTENSILIOS"] = utensiliosSeleccionados;
                }
            }

            return View();
        }
    }
}
