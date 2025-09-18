using System.Security.Cryptography;
using System.Text;

namespace KOG.ECommerce.Helpers
{
    public static class PasswordHasher
    {
        public static string Hash(string password)
        {
            const string SecretKey = "KOG.ECommerce";
            byte[] keyBytes = Encoding.ASCII.GetBytes(SecretKey);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            using (var hmac = new HMACSHA256(keyBytes))
            {
                byte[] hashBytes = hmac.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
        public static bool Verify(string password, string storedHash)
        {
            string hashedPassword = Hash(password);
            return hashedPassword == storedHash;
        }
    }
}
