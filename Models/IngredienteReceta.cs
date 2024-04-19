namespace CupMetric.Models
{
    public class IngredienteReceta
    {
        public int IdIngredienteReceta { get; set; }
        public int IdReceta { get; set; }
        public int IdIngrediente { get; set; }
        public double Cantidad { get; set; }
    }
}
