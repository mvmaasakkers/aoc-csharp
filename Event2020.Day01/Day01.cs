using System;
using System.Collections.Generic;
using System.Linq;

namespace Event2020.Day01
{
    public class Day01
    {
        private List<int> _input;

        public Day01(string input)
        {
            _input = input.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => int.Parse(t.Trim())).ToList();
        }

        public long ComputePart1()
        {
            return _input.SelectMany(l => _input, (l, r) => (l, r))
                .Where(t => t.l + t.r == 2020)
                .Select(t => t.l * t.r)
                .First();
        }

        public long ComputePart2()
        {
            return _input.SelectMany(l => _input, (l, r) => new {l, r})
                .SelectMany(t => _input, (t, m) => new {t, m})
                .Where(t => t.t.l + t.t.r + t.m == 2020)
                .Select(t => t.t.l * t.t.r * t.m)
                .First();
        }
    }
}