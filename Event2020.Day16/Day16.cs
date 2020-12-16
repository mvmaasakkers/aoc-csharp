using System;
using System.Collections.Generic;
using System.Linq;

namespace Event2020.Day16
{
    public class Day16
    {
        private List<string> _input;

        public Day16(string input)
        {
            _input = input.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim()).ToList();
        }

        public long ComputePart1()
        {
            return 0;
        }

        public long ComputePart2()
        {
            return 0;
        }
    }
}