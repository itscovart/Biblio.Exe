using System.Security.Cryptography;
using System.Text;

namespace BiblioExe.Utilerias
{
    public class Encriptar
    {
        public static string HashString(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public static bool VerifyHash(string input, string hash)
        {
            string hashedInput = HashString(input);
            return StringComparer.OrdinalIgnoreCase.Compare(hashedInput, hash) == 0;
        }

    }
}
