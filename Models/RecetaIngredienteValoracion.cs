namespace CupMetric.Models
{
    public class RecetaIngredienteValoracion
    {
        public List<int> IdIngrediente { get; set; }
        public List<string> NombreIngrediente { get; set; }
        public List<int> Cantidad { get; set; }
        public List<bool> Medible { get; set; }

        public Receta Receta { get; set; }
        public int Valoracion { get; set; }
    }
}
