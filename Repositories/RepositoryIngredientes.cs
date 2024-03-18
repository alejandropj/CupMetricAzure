using CupMetric.Data;
using CupMetric.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CupMetric.Repositories
{
    public class RepositoryIngredientes
    {
        private CupMetricContext context;
        public RepositoryIngredientes(CupMetricContext context)
        {
            this.context = context;
        }
        public async Task<List<Ingrediente>> GetIngredientesAsync()
        {
            List<Ingrediente> ingredientes = await this.context.Ingredientes.ToListAsync();
            return ingredientes;
        }             
        public async Task<List<Ingrediente>> GetIngredientesMediblesAsync()
        {
            var consulta = from datos in this.context.Ingredientes 
                           where datos.Medible==true select datos;
            List < Ingrediente > ingredientes = await consulta.ToListAsync();
            return ingredientes;
        }        
        public async Task<int> CountIngredientesAsync()
        {
            return await this.context.Ingredientes.CountAsync();
        }
        public async Task<Ingrediente> FindIngredienteByIdAsync(int idIngrediente)
        {
            var consulta = from datos in this.context.Ingredientes
                           where datos.IdIngrediente == idIngrediente
                           select datos;
            return consulta.AsEnumerable().FirstOrDefault();
        }
        public async Task CreateIngredienteAsync(Ingrediente ingrediente)
        {
            this.context.Ingredientes.Add(ingrediente);
            this.context.SaveChanges();
        }
        public async Task UpdateIngredienteAsync(Ingrediente ingrediente)
        {
            Ingrediente ingredienteOld = await this.FindIngredienteByIdAsync(ingrediente.IdIngrediente);
            ingredienteOld.Nombre = ingrediente.Nombre;
            ingredienteOld.Densidad = ingrediente.Densidad;
            ingredienteOld.Imagen = ingrediente.Imagen;
            ingredienteOld.Almacenamiento = ingrediente.Almacenamiento;
            ingredienteOld.Sustitutivo = ingrediente.Sustitutivo;
            ingredienteOld.Medible = ingrediente.Medible;
            this.context.SaveChanges();
        }
        public async Task DeleteIngredienteAsync(int idIngrediente)
        {
            Ingrediente ingrediente = await this.FindIngredienteByIdAsync(idIngrediente);
            this.context.Ingredientes.Remove(ingrediente);
            this.context.SaveChanges();
        }
    }
}
