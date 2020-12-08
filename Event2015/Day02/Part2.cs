using System;
using System.Collections.Generic;
using Shared;
using System.Linq;

namespace Event2015.Day02
{
    public class Part2
    {
        private List<Input> _input;
        
        public Part2(string input)
        {
           _input = input.Split(new string[] {"\r\n", "\n"}, StringSplitOptions.None).Select(c => new Input(c))
               .ToList();
        }

        public long Compute()
        {
            var calcs = _input.Select(c => (c.H * c.W * c.L) + (2* c.GetShortestPerimiter())).Aggregate((cur, val) => cur + val);
            
            return calcs;
        }
    }
}