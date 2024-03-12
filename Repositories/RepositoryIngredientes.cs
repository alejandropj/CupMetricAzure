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
            string sql = "INSERT INTO INGREDIENTE VALUES (NULL, @NOMBRE, @DENSIDAD, @IMAGEN, @ALMACENAMIENTO, @SUSTITUTIVO, @Medible)";
            SqlParameter pamNombre = new SqlParameter("@NOMBRE", ingrediente.Nombre);
            SqlParameter pamDensidad = new SqlParameter("@DENSIDAD", ingrediente.Densidad);
            SqlParameter pamImagen = new SqlParameter("@IMAGEN", ingrediente.Imagen);
            SqlParameter pamAlmacenamiento = new SqlParameter("@ALMACENAMIENTO", ingrediente.Almacenamiento);
            SqlParameter pamSustitutivo = new SqlParameter("@SUSTITUTIVO", ingrediente.Sustitutivo);
            SqlParameter pamSinonimo = new SqlParameter("@Medible", ingrediente.Medible);

            int af = await this.context.Database.ExecuteSqlRawAsync(sql, pamNombre, 
                pamDensidad, pamImagen, pamAlmacenamiento, pamSustitutivo, pamSinonimo);
        }
        public async Task UpdateIngredienteAsync(Ingrediente ingrediente)
        {
            string sql = "UPDATE INGREDIENTE SET NOMBRE=@NOMBRE, DENSIDAD= @DENSIDAD, " +
                "IMAGEN=@IMAGEN, ALMACENAMIENTO = @ALMACENAMIENTO, SUSTITUTIVO= @SUSTITUTIVO, " +
                "Medible = @Medible";
            SqlParameter pamNombre = new SqlParameter("@NOMBRE", ingrediente.Nombre);
            SqlParameter pamDensidad = new SqlParameter("@DENSIDAD", ingrediente.Densidad);
            SqlParameter pamImagen = new SqlParameter("@IMAGEN", ingrediente.Imagen);
            SqlParameter pamAlmacenamiento = new SqlParameter("@ALMACENAMIENTO", ingrediente.Almacenamiento);
            SqlParameter pamSustitutivo = new SqlParameter("@SUSTITUTIVO", ingrediente.Sustitutivo);
            SqlParameter pamSinonimo = new SqlParameter("@Medible", ingrediente.Medible);

            int af = await this.context.Database.ExecuteSqlRawAsync(sql, pamNombre,
                pamDensidad, pamImagen, pamAlmacenamiento, pamSustitutivo, pamSinonimo);
        }
        public async Task DeleteIngredienteAsync(int idIngrediente)
        {
            string sql = "DELETE FROM INGREDIENTE WHERE IDINGREDIENTE = @IDINGREDIENTE";
            SqlParameter pamId = new SqlParameter("@IDINGREDIENTE", idIngrediente);
            int af = await this.context.Database.ExecuteSqlRawAsync(sql, pamId);
        }
    }
}
