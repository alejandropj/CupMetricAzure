using CupMetric.Data;
using CupMetric.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CupMetric.Repositories
{
    public class RepositoryReceta
    {
        private CupMetricContext context;
        public RepositoryReceta(CupMetricContext context)
        {
            this.context = context;
        }
        public async Task<List<Receta>> GetRecetasAsync()
        {
            var consulta = await this.context.Recetas.ToListAsync();
            return consulta;
        }        
        public async Task<int> CountRecetasAsync()
        {
            var consulta = this.context.Recetas.CountAsync();
            return (int)consulta.Result;
        }
        public async Task<Receta> FindRecetaByIdAsync(int idReceta)
        {
            var consulta = from datos in this.context.Recetas
                           where datos.IdReceta == idReceta
                           select datos;
            return consulta.AsEnumerable().FirstOrDefault();
        }
        public async Task CreateRecetaAsync(Receta receta)
        {
            string sql = "INSERT INTO USUARIO VALUES (NULL, @NOMBRE, @INSTRUCCIONES, " +
                "@IMAGEN, @IDCATEGORIA, @TIEMPO, 0)";
            SqlParameter pamNombre = new SqlParameter("@NOMBRE", receta.Nombre);
            SqlParameter pamInstrucciones = new SqlParameter("@INSTRUCCIONES", receta.Instrucciones);
            SqlParameter pamImagen = new SqlParameter("@IMAGEN", receta.Imagen);
            SqlParameter pamIdCategoria = new SqlParameter("@IDCATEGORIA", receta.IdCategoria);
            SqlParameter pamTiempo = new SqlParameter("@TIEMPO", receta.TiempoPreparacion);
            int af = await this.context.Database.ExecuteSqlRawAsync(sql, pamNombre, pamInstrucciones,
                pamImagen, pamIdCategoria, pamTiempo);
        }
        public async Task UpdateRecetaAsync(Receta receta)
        {
            string sql = "UPDATE RECETA SET NOMBRE= @NOMBRE, INSTRUCCIONES = @INSCRUCCIONES" +
                ", IMAGEN = @IMAGEN, IDCATEGORIA = @IDCATEGORIA, TIEMPOPREPARACION = @TIEMPO," +
                "VISITAS = @VISITAS WHERE IDRECETA = @IDRECETA";
            SqlParameter pamNombre = new SqlParameter("@NOMBRE", receta.Nombre);
            SqlParameter pamInstrucciones = new SqlParameter("@INSTRUCCIONES", receta.Instrucciones);
            SqlParameter pamImagen = new SqlParameter("@IMAGEN", receta.Imagen);
            SqlParameter pamIdCategoria = new SqlParameter("@IDCATEGORIA", receta.IdCategoria);
            SqlParameter pamTiempo = new SqlParameter("@TIEMPO", receta.TiempoPreparacion);
            SqlParameter pamId = new SqlParameter("@IDRECETA", receta.IdReceta);
            int af = await this.context.Database.ExecuteSqlRawAsync(sql, pamNombre, pamInstrucciones,
                pamImagen, pamIdCategoria, pamTiempo, pamId);
        }
        public async Task DeleteRecetaAsync(int IdReceta)
        {
            string sql = "DELETE FROM RECETA WHERE IDRECETA = @IDRECETA";
            SqlParameter pamId = new SqlParameter("@IDRECETA", IdReceta);
            int af = await this.context.Database.ExecuteSqlRawAsync(sql, pamId);
        }

        public async Task<List<Receta>> FilterRecetaByCategoriaAsync(int IdCategoria)
        {
            string sql = "SELECT * FROM RECETA WHERE IDCATEGORIA=@IDCATEGORIA";
            SqlParameter pamId = new SqlParameter("@IDCATEGORIA", IdCategoria);

            var receta = await this.context.Recetas.FromSqlRaw(sql, pamId).ToListAsync();
            return receta;
        }
        //Completar
        public async Task<List<Receta>> FilterRecetaByIngredienteAsync(int[] IdIngredientes)
        {
            string sql = "SELECT * FROM RECETA";
            var consulta = new List<Receta>();
            foreach (var id in IdIngredientes)
            {
                var receta = await this.context.Recetas.FromSqlRaw(sql).Where(r => r.IdCategoria == id).ToListAsync();
                consulta.AddRange(receta);
            }

            return consulta;
        }
    }
}
