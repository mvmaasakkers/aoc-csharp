using System.Security.Cryptography;
using System.Text;

namespace Event2015.Day04
{
    public static class FindHash
    {
        public static long Find(string input, int len)
        {
            var n = 1;
            var check = new string('0', len);
            while (! CalculateHash($"{input}{n}").StartsWith(check))
            {
                n++;
            }
            
            return n;
        }

        private static string CalculateHash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}