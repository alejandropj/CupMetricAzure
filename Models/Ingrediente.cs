using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CupMetric.Models
{
    [Table("INGREDIENTE")]
    public class Ingrediente
    {
        [Key]
        [Column("IdIngrediente")]
        public int IdIngrediente { get; set; }        
        [Column("Nombre")]
        public string Nombre { get; set; }
    }
}
