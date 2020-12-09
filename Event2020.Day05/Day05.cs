using System;
using System.Collections.Generic;
using System.Linq;

namespace Event2020.Day05
{
    public class Day05
    {
        private List<string> _input;

        public Day05(string input)
        {
            _input = input.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim()).ToList();
        }

        public long ComputePart1()
        {
            return _input.Select(t => new Input(t)).Select(t => (t.Row * 8) + t.Column).Max();
        }

        public long ComputePart2()
        {
            var ids = _input.Select(t => new Input(t)).Select(t => (t.Row * 8) + t.Column).OrderBy(t => t).ToList();
            for (int i = 0; i < ids.Count; i++)
            {
                if (ids[i + 1] - ids[i] != 1)
                {
                    return ids[i] + 1;
                }
            }

            return 0;
        }
    }

    public class Input
    {
        public Input(string raw)
        {
            Row = Convert.ToInt32(raw.Substring(0, 7).Replace('B', '1').Replace('F', '0'), 2);
            Column = Convert.ToInt32(raw.Substring(7, 3).Replace('R', '1').Replace('L', '0'), 2);
        }

        public int Row { get; set; }

        public int Column { get; set; }
    }
}