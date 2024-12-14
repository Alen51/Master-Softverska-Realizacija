using Org.BouncyCastle.Crypto.Generators;
namespace SoftverskaRealizacijaBackend.Helpers
{
    public class ClientHelper
    {
        public static string HashPassword(string password)
        {
            return crypto.Security.ComputeHash(password, "aaa");
        }
    }
}
