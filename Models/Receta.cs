namespace CupMetric.Models
{
    public class Receta
    {
        public int IdReceta { get; set; }
        public string Nombre { get; set; }
        public string? Instrucciones { get; set; }
        public string? Imagen { get; set; }
        public int IdCategoria { get; set; }
        public int TiempoPreparacion { get; set; }
        public int Visitas { get; set; }        
        public double? MediaValoraciones { get; set; }
    }
}
