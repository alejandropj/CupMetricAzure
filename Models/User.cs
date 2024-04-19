namespace CupMetric.Models
{
    public class User
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }        
        public string Salt { get; set; }
        public byte[] Password { get; set; }        
        public int IdRol { get; set; }
    }
}
