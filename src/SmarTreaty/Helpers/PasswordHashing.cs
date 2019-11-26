using System.Security.Cryptography;
using System.Text;
using SmarTreaty.Helpers.Extensions;

namespace SmarTreaty.Helpers
{
    public static class PasswordHashing
    {
        public static string GetPasswordHash(string password, string passwordSalt)
        {
            using (var sha = SHA512.Create())
            {
                var passwordData = password.Xor(passwordSalt);
                var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(passwordData));
                var sb = new StringBuilder(128);
                foreach (var b in hash)
                {
                    sb.Append(b.ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}