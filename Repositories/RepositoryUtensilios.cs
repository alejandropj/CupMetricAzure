using CupMetric.Data;
using CupMetric.Models;
using Microsoft.EntityFrameworkCore;

namespace CupMetric.Repositories
{
    public class RepositoryUtensilios
    {
        private CupMetricContext context;
        public RepositoryUtensilios(CupMetricContext context)
        {
            this.context = context;
        }
        public async Task<List<Utensilio>> GetUtensiliosAsync()
        {
            List<Utensilio> utensilios = await this.context.Utensilios.ToListAsync();
            return utensilios;
        }        
        public async Task<int> CountUtensiliosAsync()
        {
            return await this.context.Utensilios.CountAsync();
        }
        public async Task<Utensilio> FindUtensilioByIdAsync(int idUtensilio)
        {
            var consulta = from datos in this.context.Utensilios
                           where datos.IdUtensilio == idUtensilio
                           select datos;
            return consulta.AsEnumerable().FirstOrDefault();
        }
    }
}
