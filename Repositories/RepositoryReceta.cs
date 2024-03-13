using CupMetric.Data;
using CupMetric.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics.Metrics;
using System.Runtime.Intrinsics.X86;

namespace CupMetric.Repositories
{
    #region Procedimientos Almacenados
/*  CREATE PROCEDURE SP_RECETA_VALORACION_INGREDIENTES
    (@IDRECETA INT, @VALORACION INT OUT)
    AS
	    SELECT @VALORACION = ISNULL(AVG(VALORACION), 1) FROM VALORACIONES WHERE IDRECETA=@IDRECETA

	    SELECT IR.IDINGREDIENTE, I.NOMBRE, IR.CANTIDAD, I.MEDIBLE
        FROM INGREDIENTE_RECETA AS IR
        INNER JOIN INGREDIENTE AS I ON IR.IDINGREDIENTE = I.IDINGREDIENTE
        WHERE IR.IDRECETA = @IDRECETA;
    GO*/
    #endregion
    public class RepositoryReceta
    {
        private SqlConnection cn;
        private SqlCommand com;
        private SqlDataReader reader;

        private CupMetricContext context;
        public RepositoryReceta(CupMetricContext context)
        {
            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=CUPMETRIC;Persist Security Info=True;User ID=sa;Password=MCSD2023;Trust Server Certificate=true";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
            this.context = context;
        }
        public async Task<List<Receta>> GetRecetasAsync()
        {
            var consulta = await this.context.Recetas.ToListAsync();
            return consulta;
        }
        public async Task<List<RecetaIngredienteValoracion>> GetRecetasFormattedAsync()
        {
            var consulta = await this.context.Recetas.ToListAsync();
            List<RecetaIngredienteValoracion> recetasFormateadas = new List<RecetaIngredienteValoracion>();
            foreach (Receta rec in consulta)
            {
                string sql = "SP_RECETA_VALORACION_INGREDIENTES";
                this.com.Parameters.AddWithValue("@IDRECETA", rec.IdReceta);
                SqlParameter pamValoracion = new SqlParameter("@VALORACION", 0);
                pamValoracion.Direction = ParameterDirection.Output;
                this.com.Parameters.Add(pamValoracion);
                this.com.CommandType = CommandType.StoredProcedure;
                this.com.CommandText = sql;
                this.cn.Open();
                this.reader = this.com.ExecuteReader();

                RecetaIngredienteValoracion recetasForm = new RecetaIngredienteValoracion();
                recetasForm.IdIngrediente = new List<int>();
                recetasForm.NombreIngrediente = new List<string>();
                while (this.reader.Read())
                {
                    recetasForm.IdIngrediente.Add(int.Parse(this.reader["IDINGREDIENTE"].ToString()));
                    recetasForm.NombreIngrediente.Add(this.reader["NOMBRE"].ToString());

                }
                this.reader.Close();
                recetasForm.Valoracion = int.Parse(pamValoracion.Value.ToString());
                recetasForm.Receta = rec;
                this.com.Parameters.Clear();
                this.cn.Close();
                recetasFormateadas.Add(recetasForm);

            }
            return recetasFormateadas;
        }
        public async Task<RecetaIngredienteValoracion> FindRecetaFormattedAsync(int idReceta)
        {
            
            string sql = "SP_RECETA_VALORACION_INGREDIENTES";
            this.com.Parameters.AddWithValue("@IDRECETA", idReceta);
            SqlParameter pamValoracion = new SqlParameter("@VALORACION", 0);
            pamValoracion.Direction = ParameterDirection.Output;
            this.com.Parameters.Add(pamValoracion);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            this.cn.Open();
            this.reader = this.com.ExecuteReader();

            RecetaIngredienteValoracion recetaForm = new RecetaIngredienteValoracion();
            recetaForm.IdIngrediente = new List<int>();
            recetaForm.NombreIngrediente = new List<string>();
            recetaForm.Cantidad = new List<int>();
            recetaForm.Medible = new List<bool>();
            while (this.reader.Read())
            {
                recetaForm.IdIngrediente.Add(int.Parse(this.reader["IDINGREDIENTE"].ToString()));
                recetaForm.NombreIngrediente.Add(this.reader["NOMBRE"].ToString());
                recetaForm.Cantidad.Add(int.Parse(this.reader["CANTIDAD"].ToString()));
                recetaForm.Medible.Add(bool.Parse(this.reader["MEDIBLE"].ToString()));

            }
            this.reader.Close();
            recetaForm.Valoracion = int.Parse(pamValoracion.Value.ToString());
            var consulta = from datos in this.context.Recetas
                           where datos.IdReceta == idReceta
                           select datos;
            recetaForm.Receta = consulta.AsEnumerable().FirstOrDefault();

            this.com.Parameters.Clear();
            this.cn.Close();


            return recetaForm;
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
        public async Task AddVisitRecetaAsync(int idReceta)
        {
            string sql = "UPDATE RECETA SET VISITAS=VISITAS+1 WHERE IDRECETA = @IDRECETA";
            SqlParameter pamId = new SqlParameter("@IDRECETA", idReceta);
            int af = await this.context.Database.ExecuteSqlRawAsync(sql, pamId);
        }

        public async Task<List<Categoria>> GetCategoriasAsync()
        {
            var consulta = await this.context.Categorias.ToListAsync();
            return consulta;
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

        public async Task<bool> PostValoracionAsync(int idReceta, int idUsuario, int valoracion)
        {
            //string sql = "SELECT IDVALORACION FROM VALORACIONES WHERE IDRECETA=@IDRECETA AND IDUSUARIO=@IDUSUARIO";
            var consulta = from datos in this.context.Valoraciones
                           where datos.IdReceta == idReceta && datos.IdUsuario== idUsuario
                           select datos.IdValoracion;
            if (consulta == null)
            {
                string sql = "INSERT INTO VALORACIONES VALUES(NULL, @IDRECETA, @IDUSUARIO, @VALORACION)";
                SqlParameter pamIdReceta = new SqlParameter("@IDRECETA", idReceta);
                SqlParameter pamIdUsuario = new SqlParameter("@IDUSUARIO", idUsuario);
                int af = await this.context.Database.ExecuteSqlRawAsync(sql, pamIdReceta, pamIdUsuario);
                return false;
            }
            else
            {
                return true;
            }
      
        }

        public async Task<List<Ingrediente>> FindIngredientesbyReceta(int idReceta)
        {
            /*            var consulta = from datos in this.context.IngredientesReceta
                                       where datos.IdReceta == idReceta
                                       select datos;*/

            var ingredientes = from ingReceta in this.context.IngredientesReceta
                               join ingrediente in this.context.Ingredientes on ingReceta.IdIngrediente equals ingrediente.IdIngrediente
                               where ingReceta.IdReceta == idReceta
                               select ingrediente;

            return await ingredientes.ToListAsync();
        }
    }
}
