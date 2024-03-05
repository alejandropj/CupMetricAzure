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
            string sql = "SELECT * FROM UTENSILIOS";
            var consulta = this.context.Utensilios.FromSqlRaw(sql);
            return await consulta.ToListAsync();
        }
    }
}
