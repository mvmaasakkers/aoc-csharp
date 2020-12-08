using System;
using System.Collections.Generic;
using Shared;
using System.Linq;

namespace Event2015.Day02
{
    public class Part1
    {
        private List<Input> _input;

        public Part1(string input)
        {
            _input = input.Split(new string[] {"\r\n", "\n"}, StringSplitOptions.None).Select(c => new Input(c))
                .ToList();
        }

        public long Compute()
        {
            return _input.Select(c => (2 * c.L * c.W) + (2 * c.W * c.H) + (2 * c.L * c.H) + c.GetSmallestSide())
                .Aggregate((cur, val) => cur + val);
        }
    }
}