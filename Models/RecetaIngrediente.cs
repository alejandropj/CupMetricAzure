namespace CupMetric.Models
{
    public class RecetaIngrediente
    {
        public List<int> IdIngredientes { get; set; }
        public List<double> Cantidad { get; set; }

        public Receta Receta { get; set; }
    }
}
