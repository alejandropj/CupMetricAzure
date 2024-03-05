using System.Security.Cryptography;

namespace CupMetric.Helpers
{
    public static class HelperEncrypt
    {
        public static byte[] EncryptPassword(string password, string salt)
        {
            string contenido = password+salt;
            SHA512 sha = SHA512.Create();
            byte[] salida = System.Text.Encoding.UTF8.GetBytes(contenido);
            for (int i = 0; i <= 114; i++)
            {
                salida = sha.ComputeHash(salida);
            }
            sha.Clear();
            return salida;
        }
    }
}
