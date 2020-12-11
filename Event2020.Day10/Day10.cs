using System;
using System.Collections.Generic;
using System.Linq;

namespace Event2020.Day10
{
    public class Day10
    {
        private List<int> _input;

        public Day10(string input)
        {
            _input = input.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => int.Parse(t.Trim())).ToList();
            _input.Add(0);
            _input.Add(_input.Max() + 3);
            _input.Sort();
        }

        public long ComputePart1()
        {
            // I created this solution after submitting the answer. When i was done I looked for a better way 
            // of doing this in Linq and I worked one out! See ComputePart1Original for the first version.
            var l = _input.Zip(_input.ToArray()[1..]).Select(t => t.Second - t.First).ToArray();
            return l.Count(t => t == 1) * l.Count(t => t == 3);
        }

        public long ComputePart2()
        {
            var opts = new Dictionary<int, long>();
            opts[_input.Count - 1] = 1;

            for (var k = _input.Count - 2; k >= 0; k--)
            {
                long currentCount = 0;
                for (var connected = k + 1; connected < _input.Count && _input[connected] - _input[k] <= 3; connected++)
                {
                    currentCount += opts[connected];
                }

                opts[k] = currentCount;
            }

            return opts[0];
        }

        public long ComputePart1Original()
        {
            var outlet = 0;
            var one = 0;
            var three = 0;

            var running = true;
            while (running)
            {
                var f = _input.Where(t => t == outlet + 1).FirstOrDefault();
                if (f > 0)
                {
                    one++;
                    outlet = f;
                }
                else
                {
                    var f2 = _input.Where(t => t == outlet + 3).FirstOrDefault();
                    if (f2 > 0)
                    {
                        three++;
                        outlet = f2;
                    }
                }

                if (outlet >= _input.Max())
                {
                    running = false;
                }
            }

            return one * three;
        }
    }
}
