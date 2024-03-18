using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

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
        [Column("Densidad")]
        public double? Densidad { get; set; }
        [Column("Imagen")]
        public string? Imagen { get; set; }     
        [Column("Almacenamiento")]
        public string? Almacenamiento { get; set; }     
        [Column("Sustitutivo")]
        public int? Sustitutivo { get; set; }     
        [Column("Medible")]
        public bool Medible { get; set; }
    }
}
