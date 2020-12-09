using System;
using System.Collections.Generic;
using System.Linq;

namespace Event2015.Day07
{
    public class Day07
    {
        private List<string> _input;

        public Day07(string input)
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