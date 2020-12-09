using System;
using System.Collections.Generic;
using System.Linq;

namespace Event2020.Day09
{
    public class Day09
    {
        private List<long> _input;

        public Day09(string input)
        {
            _input = input.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => long.Parse(t.Trim())).ToList();
        }

        public long ComputePart1()
        {
            var offset = 0;
            var preamble = 25;

            for (int i = preamble; i < _input.Count; i++)
            {
                var preambleListL = _input.GetRange(offset, preamble);
                var preambleListR = _input.GetRange(offset, preamble);

                if (!found(preambleListL, preambleListR, _input[i]))
                {
                    return _input[i];
                }

                offset++;
            }

            return 0;
        }

        public long ComputePart2()
        {
            var answerPart1 = ComputePart1();
            var offset = 0;

            foreach (var i in _input)
            {
                var count = 2;
                for (int j = offset; j < _input.Count; j++)
                {
                    var range = _input.GetRange(offset, count);
                    if (range.Sum() == answerPart1)
                    {
                        return range.Min() + range.Max();
                    }

                    if (range.Sum() > answerPart1)
                    {
                        break;
                    }

                    count++;
                }

                offset++;
            }

            return 0;
        }

        private bool found(List<long> ll, List<long> lr, long input)
        {
            var found = false;

            foreach (var l in ll)
            {
                foreach (var r in lr)
                {
                    if (l + r == input && l != r)
                    {
                        found = true;
                    }
                }
            }

            return found;
        }
    }
}