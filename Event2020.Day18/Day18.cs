using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Event2020.Day18
{
    public class Day18
    {
        private List<string> _input;

        public Day18(string input)
        {
            _input = input.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim()).ToList();
        }

        public long ComputePart1()
        {
            return _input.Sum(l => Calculate(PlusSigns(false, Brackets(false, l))));
        }

        
        public long ComputePart2()
        {
            return _input.Sum(l => Calculate(PlusSigns(true, Brackets(true, l))));
        }
        
        private static long Calculate(string sum)
        {
            var bits = sum.Split(new[] { ' ', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            var result = long.Parse(bits[0]);
            for (var i = 0; i < bits.Length - 2; i += 2)
            {
                result = bits[i + 1] == "*" ? result * long.Parse(bits[i + 2]) : result += long.Parse(bits[i + 2]);
            }

            return result;
        }

        private string Brackets(bool part2, string v)
        {
            while (v.Contains("("))
            {
                var bracket = Regex.Match(v, @"(\((?:[^\(]*?\)))").ToString();
                v = v.Replace(bracket, Calculate(PlusSigns(part2, bracket)).ToString());
            }
            return v;
        }

        private string PlusSigns(bool part2, string v)
        {
            if (!part2) return v;
            while (v.Contains("+"))
            {
                Match match = Regex.Match(v, @"(\d+ \+ \d+)");
                v = v.Substring(0, match.Index) + Calculate(match.Value) + v.Substring(match.Index + match.Value.Length);
            }
            return v;
        }
    }
}