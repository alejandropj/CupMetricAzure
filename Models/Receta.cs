using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CupMetric.Models
{
    [Table("RECETA")]
    public class Receta
    {
        [Key]
        [Column("IDRECETA")]
        public int IdReceta { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
        [Column("INSTRUCCIONES")]
        public string Instrucciones { get; set; }
        [Column("IMAGEN")]
        public string Imagen { get; set; }
        [Column("IDCATEGORIA")]
        public int IdCategoria { get; set; }
        [Column("TIEMPOPREPARACION")]
        public int TiempoPreparacion { get; set; }
        [Column("VISITAS")]
        public int Visitas { get; set; }
    }
}
