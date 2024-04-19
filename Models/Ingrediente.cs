namespace CupMetric.Models
{
    public class Ingrediente
    {
        public int IdIngrediente { get; set; }        
        public string Nombre { get; set; }
        public double? Densidad { get; set; }
        public string? Imagen { get; set; }     
        public string? Almacenamiento { get; set; }     
        public int? Sustitutivo { get; set; }     
        public bool Medible { get; set; }
    }
}
