using System;
using System.Security.Cryptography;
using System.Text;

namespace SecurityLib
{
    public static class Security
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static bool VerifyHash(string input, string hash)
        {
            string hashOfInput = HashPassword(input);
            return string.Equals(hashOfInput, hash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
