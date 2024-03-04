using CupMetric.Data;
using CupMetric.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CupMetric.Repositories
{
    public class RepositoryUsers
    {
        private CupMetricContext context;
        public RepositoryUsers(CupMetricContext context)
        {
            this.context = context;
        }
        public async Task<List<User>> GetUsers()
        {
            string sql = "SELECT * FROM USUARIO";
            var consulta = this.context.Users.FromSqlRaw(sql);
            return await consulta.ToListAsync();
        }             
        public async Task<int> CountUsers()
        {
            var consulta = this.context.Users.CountAsync();
            int res = (int)consulta.Result;
            return res;
        }        
        public async Task<User> FindUserById(int IdUser)
        {
            string sql = "SELECT * FROM USUARIO WHERE IDUSUARIO = @IDUSUARIO";
            SqlParameter pamId = new SqlParameter("@IDUSUARIO", IdUser);
            var consulta = this.context.Users.FromSqlRaw(sql, pamId);
            User usuario = consulta.AsEnumerable().FirstOrDefault();
            return usuario;
        }        
        public async Task CreateUser(User usuario)
        {
            string sql = "INSERT INTO USUARIO VALUES (NULL, @NOMBRE, @EMAIL, @PASSWORD, 2)";
            SqlParameter pamNombre = new SqlParameter("@NOMBRE", usuario.Nombre);
            SqlParameter pamEmail = new SqlParameter("@EMAIL", usuario.Email);
            SqlParameter pamPassword = new SqlParameter("@PASSWORD", usuario.Password);
            int af = await this.context.Database.ExecuteSqlRawAsync(sql, pamNombre, pamEmail, pamPassword);
        }        
        public async Task UpdateUser(User usuario)
        {
            string sql = "UPDATE USUARIO SET NOMBRE= @NOMBRE, EMAIL= @EMAIL, PASSWORD = @PASSWORD WHERE IDUSUARIO = @IDUSUARIO";
            SqlParameter pamId = new SqlParameter("@IDUSUARIO", usuario.IdUsuario);
            SqlParameter pamNombre = new SqlParameter("@NOMBRE", usuario.Nombre);
            SqlParameter pamEmail = new SqlParameter("@EMAIL", usuario.Email);
            SqlParameter pamPassword = new SqlParameter("@PASSWORD", usuario.Password);
            int af = await this.context.Database.ExecuteSqlRawAsync(sql, pamId, pamNombre, pamEmail, pamPassword);
        }        
        public async Task DeleteUser(int idUsuario)
        {
            string sql = "DELETE FROM USUARIO WHERE IDUSUARIO = @IDUSUARIO";
            SqlParameter pamId = new SqlParameter("@IDUSUARIO", idUsuario);
            int af = await this.context.Database.ExecuteSqlRawAsync(sql, pamId);
        }
    }
}
