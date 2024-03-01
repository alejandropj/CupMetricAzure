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
        public async Task<User> FindUserById(int IdUser)
        {
            string sql = "SELECT * FROM USUARIO WHERE IDUSUARIO = @IDUSUARIO";
            SqlParameter pamId = new SqlParameter("@IDUSUARIO", IdUser);
            var consulta = this.context.Users.FromSqlRaw(sql, pamId);
            User usuario = consulta.AsEnumerable().FirstOrDefault();
            return usuario;
        }
    }
}
