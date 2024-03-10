using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CupMetric.Models
{
    [Table("INGREDIENTE_RECETA")]
    public class IngredienteReceta
    {
        [Key]
        [Column("IdIngrediente_Receta")]
        public int IdIngredienteReceta { get; set; }/*
        [ForeignKey("Receta")]*/
        [Column("IdReceta")]
        public int IdReceta { get; set; }/*
        [ForeignKey("Ingrediente")]*/
        [Column("IdIngrediente")]
        public int IdIngrediente { get; set; }
        [Column("Cantidad")]
        public double Cantidad { get; set; }
    }
}
