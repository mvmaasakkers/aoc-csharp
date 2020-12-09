using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Event2015.Day04
{
    public class Day04
    {
        private string _input;

        public Day04(string input)
        {
            _input = input.Trim();
        }

        public long ComputePart1()
        {
            return FindHash.Find(_input, 5);
        }

        public long ComputePart2()
        {
            return FindHash.Find(_input, 6);
        }
    }
    
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