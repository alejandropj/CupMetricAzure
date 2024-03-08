using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CupMetric.Models
{
    [Table("VALORACIONES")]
    public class Valoracion
    {
        [Key]
        [Column("IDVALORACION")]
        public int IdValoracion { get; set; }
        [Column("IDRECETA")]
        public int IdReceta { get; set; }
        [Column("IDUSUARIO")]
        public int IdUsuario { get; set; }
        [Column("VALORACION")]
        public int NumValoracion { get; set; }
    }
}
