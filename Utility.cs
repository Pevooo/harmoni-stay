using System.Security.Cryptography;
using System.Text;

namespace MainProject
{
    public static class Utility
    {

        public static string Hash(string password)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        public static bool CheckHash(string password, string hash)
        {
            return Hash(password) == hash;
        }


    }
}
