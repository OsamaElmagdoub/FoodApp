using System.Security.Cryptography;
using System.Text;

namespace ProjectManagementSystem.Helper
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            var sha256 = SHA256.Create();

            var bytes = Encoding.UTF8.GetBytes(password);

            var hashBytes = sha256.ComputeHash(bytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
        public static bool checkPassword(string password ,string storedPassword)
        {
            var hashedPassword = HashPassword(password);

            return hashedPassword == storedPassword;
        }
    }
}
