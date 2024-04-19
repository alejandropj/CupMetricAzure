/*using CupMetric.Data;
using CupMetric.Models;
using Microsoft.Data.SqlClient;
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
        public async Task CreateUtensilioAsync(Utensilio utensilio)
        {
            string sql = "INSERT INTO UTENSILIO VALUES (@NOMBRE, @VOLUMEN, @IMAGEN, @RECOMENDACION)";
            SqlParameter pamNombre = new SqlParameter("@NOMBRE", utensilio.Nombre);
            SqlParameter pamVolumen = new SqlParameter("@VOLUMEN", utensilio.Volumen);
            SqlParameter pamImagen = new SqlParameter("@IMAGEN", utensilio.Imagen);
            SqlParameter pamRecomendacion = new SqlParameter("@RECOMENDACION", utensilio.Recomendacion);

            int af = await this.context.Database.ExecuteSqlRawAsync(sql, pamNombre,
                pamVolumen, pamImagen, pamRecomendacion);
        }
        public async Task UpdateUtensilioAsync(Utensilio utensilio)
        {
            Utensilio utensilioOld = await this.FindUtensilioByIdAsync(utensilio.IdUtensilio);
            utensilioOld.Nombre = utensilio.Nombre;
            utensilioOld.Volumen = utensilio.Volumen;
            utensilioOld.Imagen = utensilio.Imagen;
            utensilioOld.Recomendacion = utensilio.Recomendacion;
            this.context.SaveChangesAsync();
        }
        public async Task DeleteUtensilioAsync(int idUtensilio)
        {
            string sql = "DELETE FROM UTENSILIO WHERE IDUTENSILIO = @ID";
            SqlParameter pamId = new SqlParameter("@ID", idUtensilio);
            int af = await this.context.Database.ExecuteSqlRawAsync(sql, pamId);
        }
    }
}
*/