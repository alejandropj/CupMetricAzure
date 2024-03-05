using CupMetric.Data;
using CupMetric.Helpers;
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
        public async Task<User> LoginUserAsync(string email, string password)
        {
            User user = await this.context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user != null)
            {
                string salt = user.Salt;
                byte[] passTemp = HelperEncrypt.EncryptPassword(password, salt);
                byte[] passUser = user.Password;
                bool response = HelperTools.CompareArrays(passTemp, passUser);
                if (response)
                {
                    return user;
                }
            }
            return null;
        }
        public async Task RegisterUserAsync(string nombre, string email, string password)
        {
            User user = new User();
            user.IdUsuario = 0;
            user.Nombre = nombre;
            user.Email = email;
            user.Salt = HelperTools.GenerateSalt();
            user.Password = HelperEncrypt.EncryptPassword(password, user.Salt);
            user.IdRol = 1;
            this.context.Users.Add(user);
            await this.context.SaveChangesAsync();
            //return user;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var consulta = await this.context.Users.ToListAsync();
            return consulta;
        }             
        public async Task<int> CountUsersAsync()
        {
            var consulta = this.context.Users.CountAsync();
            int count = (int)consulta.Result;
            return count;
        }        
        public async Task<User> FindUserByIdAsync(int IdUser)
        {
            var consulta = from datos in this.context.Users
                           where datos.IdUsuario == IdUser
                           select datos;
            return consulta.AsEnumerable().FirstOrDefault();
        }
    
        public async Task UpdateUserAsync(int idUser, string nombre, string email, string password)
        {
            string sql = "UPDATE USUARIO SET NOMBRE= @NOMBRE, EMAIL= @EMAIL, SALT = @SALT, PASSWORD = @PASSWORD WHERE IDUSUARIO = @IDUSUARIO";
            string salt = HelperTools.GenerateSalt();
            byte[] passwordByte = HelperEncrypt.EncryptPassword(password, salt);
            SqlParameter pamNombre = new SqlParameter("@NOMBRE", nombre);
            SqlParameter pamEmail = new SqlParameter("@EMAIL", email);
            SqlParameter pamSalt = new SqlParameter("@SALT", salt);
            SqlParameter pamPassword = new SqlParameter("@PASSWORD", passwordByte);
            SqlParameter pamId = new SqlParameter("@IDUSUARIO", idUser);
            int af = await this.context.Database.ExecuteSqlRawAsync(sql, pamNombre, pamEmail, pamSalt, pamPassword, pamId);
        }        
        public async Task DeleteUserAsync(int idUsuario)
        {
            string sql = "DELETE FROM USUARIO WHERE IDUSUARIO = @IDUSUARIO";
            SqlParameter pamId = new SqlParameter("@IDUSUARIO", idUsuario);
            int af = await this.context.Database.ExecuteSqlRawAsync(sql, pamId);
        }
    }
}
