using System;
using System.Collections.Generic;
using System.Linq;

namespace Event2020.Day06
{
    public class Day06
    {
        private List<string> _input;

        public Day06(string input)
        {
            _input = input.Split(new[] {"\r\n\r\n", "\n\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim()).ToList();
        }

        public long ComputePart1()
        {
            return _input.Select(t => t.Replace("\r\n", string.Empty).Replace("\n", string.Empty).Distinct())
                .Sum(t => t.Count());
        }

        public long ComputePart2()
        {
            return _input.Select(g => g.Split("\n", StringSplitOptions.RemoveEmptyEntries)
                    .Select(p => p.ToCharArray())
                    .Aggregate<IEnumerable<char>>((prev, next) => prev.Intersect(next)).ToList())
                .Sum(t => t.Count);
        }
    }
}