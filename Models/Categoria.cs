using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CupMetric.Models
{
    [Table("CATEGORIA")]
    public class Categoria
    {
        [Key]
        [Column("IdCategoria")]
        public int IdCategoria { get; set; }
        [Column("Nombre")]
        public string Nombre { get; set; }
        [Column("Descripcion")]
        public string Descripcion { get; set; }
    }
}
