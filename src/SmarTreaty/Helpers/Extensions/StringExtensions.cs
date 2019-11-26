using System.Text;

namespace SmarTreaty.Helpers.Extensions
{
    public static class StringExtensions
    {
        public static string Xor(this string first, string second)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < second.Length; i++)
            {
                sb.Append((char)(second[i] ^ first[(i % first.Length)]));
            }

            return sb.ToString();
        }
    }
}