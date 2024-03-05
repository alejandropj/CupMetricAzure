namespace CupMetric.Helpers
{
    public static class HelperTools
    {
        public static string GenerateSalt()
        {
            Random random = new Random();
            string salt = "";
            for (int i = 0; i <= 50; i++)
            {
                int rand = random.Next(1, 255);
                char letra = Convert.ToChar(rand);
                salt += letra;
            }
            return salt;
        }
        public static bool CompareArrays(byte[] a, byte[] b)
        {
            bool iguales = true;
            if (a.Length != b.Length)
            {
                iguales = false;
            }
            else
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i].Equals(b[i]) == false)
                    {
                        iguales = false;
                        break;
                    }
                }
            }
            return iguales;
        }
    }
}
