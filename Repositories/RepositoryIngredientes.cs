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
        public async Task<List<Ingrediente>> GetIngredientes()
        {
            string sql = "SELECT * FROM INGREDIENTE";
            var consulta = this.context.Ingredientes.FromSqlRaw(sql);
            return await consulta.ToListAsync();
        }
        public async Task<Ingrediente> FindIngredeinteById(int IdIngrediente)
        {
            string sql = "SELECT * FROM INGREDIENTE WHERE IDINGREDIENTE = @IDINGREDIENTE";
            SqlParameter pamId = new SqlParameter("@IDINGREDIENTE", IdIngrediente);
            var consulta = this.context.Ingredientes.FromSqlRaw(sql, pamId);
            Ingrediente ingrediente = consulta.AsEnumerable().FirstOrDefault();
            return ingrediente;
        }
        public async Task CreateIngrediente(Ingrediente ingrediente)
        {
            string sql = "INSERT INTO INGREDIENTE VALUES (NULL, @NOMBRE, @DENSIDAD, @IMAGEN, @ALMACENAMIENTO, @SUSTITUTIVO, @SINONIMO)";
            SqlParameter pamNombre = new SqlParameter("@NOMBRE", ingrediente.Nombre);
            SqlParameter pamDensidad = new SqlParameter("@DENSIDAD", ingrediente.Densidad);
            SqlParameter pamImagen = new SqlParameter("@IMAGEN", ingrediente.Imagen);
            SqlParameter pamAlmacenamiento = new SqlParameter("@ALMACENAMIENTO", ingrediente.Almacenamiento);
            SqlParameter pamSustitutivo = new SqlParameter("@SUSTITUTIVO", ingrediente.Sustitutivo);
            SqlParameter pamSinonimo = new SqlParameter("@SINONIMO", ingrediente.Sinonimo);

            int af = await this.context.Database.ExecuteSqlRawAsync(sql, pamNombre, 
                pamDensidad, pamImagen, pamAlmacenamiento, pamSustitutivo, pamSinonimo);
        }
        public async Task UpdateUser(Ingrediente ingrediente)
        {
            string sql = "UPDATE INGREDIENTE SET NOMBRE=@NOMBRE, DENSIDAD= @DENSIDAD, " +
                "IMAGEN=@IMAGEN, ALMACENAMIENTO = @ALMACENAMIENTO, SUSTITUTIVO= @SUSTITUTIVO, " +
                "SINONIMO = @SINONIMO)";
            SqlParameter pamNombre = new SqlParameter("@NOMBRE", ingrediente.Nombre);
            SqlParameter pamDensidad = new SqlParameter("@DENSIDAD", ingrediente.Densidad);
            SqlParameter pamImagen = new SqlParameter("@IMAGEN", ingrediente.Imagen);
            SqlParameter pamAlmacenamiento = new SqlParameter("@ALMACENAMIENTO", ingrediente.Almacenamiento);
            SqlParameter pamSustitutivo = new SqlParameter("@SUSTITUTIVO", ingrediente.Sustitutivo);
            SqlParameter pamSinonimo = new SqlParameter("@SINONIMO", ingrediente.Sinonimo);

            int af = await this.context.Database.ExecuteSqlRawAsync(sql, pamNombre,
                pamDensidad, pamImagen, pamAlmacenamiento, pamSustitutivo, pamSinonimo);
        }
        public async Task DeleteIngrediente(int idIngrediente)
        {
            string sql = "DELETE FROM INGREDIENTE WHERE IDINGREDIENTE = @IDINGREDIENTE";
            SqlParameter pamId = new SqlParameter("@IDINGREDIENTE", idIngrediente);
            int af = await this.context.Database.ExecuteSqlRawAsync(sql, pamId);
        }
    }
}
