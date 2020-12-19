using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Shared;

namespace Event2020.Day17
{
    public class Day17
    {
        private List<string> _input;

        public Day17(string input)
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