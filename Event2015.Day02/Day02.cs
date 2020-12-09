using System;
using System.Collections.Generic;
using System.Linq;

namespace Event2015.Day02
{
    public class Day02
    {
        private List<Input> _input;

        public Day02(string input)
        {
            _input = input.Split(new string[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries).Select(c => new Input(c))
                .ToList();
        }

        public long ComputePart1()
        {
            return _input.Select(c => (2 * c.L * c.W) + (2 * c.W * c.H) + (2 * c.L * c.H) + c.GetSmallestSide())
                .Aggregate((cur, val) => cur + val);
            
        }

        public long ComputePart2()
        {
            return _input.Select(c => (c.H * c.W * c.L) + (2* c.GetShortestPerimiter())).Aggregate((cur, val) => cur + val);
        }
    }

    public class Input
    {
        public Input()
        {
        }

        public Input(long l, long w, long h)
        {
            L = l;
            W = w;
            H = h;
        }

        public Input(string raw)
        {
            var parts = raw.Split("x");
            L = long.Parse(parts[0]);
            W = long.Parse(parts[1]);
            H = long.Parse(parts[2]);
        }

        public long GetSmallestSide()
        {
            var lowest = L * W;
            if (W * H < lowest)
            {
                lowest = W * H;
            }

            if (H * L < lowest)
            {
                lowest = H * L;
            }

            return lowest;
        }

        public long GetShortestPerimiter()
        {
            var lowest = L + W;
            if (W + H < lowest)
            {
                lowest = W + H;
            }

            if (H + L < lowest)
            {
                lowest = H + L;
            }

            return lowest;
        }

        public long L { get; set; }
        public long W { get; set; }
        public long H { get; set; }
    }
}