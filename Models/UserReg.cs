using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCupMetric.Models
{
    public class UserReg
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
