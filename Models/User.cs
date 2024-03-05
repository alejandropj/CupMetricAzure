using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CupMetric.Models
{
    [Table("USUARIO")]
    public class User
    {
        [Key]
        [Column("IDUSUARIO")]
        public int IdUsuario { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
        [Column("EMAIL")]
        public string Email { get; set; }        
        [Column("SALT")]
        public string Salt { get; set; }
        [Column("PASSWORD")]
        public byte[] Password { get; set; }        
        [Column("IDROL")]
        public int IdRol { get; set; }
    }
}
