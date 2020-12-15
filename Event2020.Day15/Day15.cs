using System;
using System.Collections.Generic;
using System.Linq;

namespace Event2020.Day15
{
    public class Day15
    {
        private List<int> _input;

        public Day15(string input)
        {
            _input = input.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => int.Parse(t.Trim())).ToList();
        }

        public long ComputePart1()
        {
            return Play(2020);
        }

        int Play(int rounds)
        {
            var mem = new Dictionary<int, List<int>> { };
            var lastNumber = 0;
            for (int i = 0; i < rounds; i++)
            {
                if (_input.Count - 1 >= i && !mem.ContainsKey(_input[i]))
                {
                    lastNumber = _input[i];
                }
                else if (!mem.ContainsKey(lastNumber))
                {
                    lastNumber = 0;
                }
                else if (mem.ContainsKey(lastNumber) && mem[lastNumber].Count > 1 &&
                         mem[lastNumber][mem[lastNumber].Count - 2] < i - 1)
                {
                    lastNumber = i - 1 - mem[lastNumber][mem[lastNumber].Count - 2];
                }
                else if (mem.ContainsKey(lastNumber) && mem[lastNumber].Count > 0 &&
                         mem[lastNumber][mem[lastNumber].Count - 1] == i - 1)
                {
                    lastNumber = 0;
                }

                if (!mem.ContainsKey(lastNumber))
                {
                    mem[lastNumber] = new List<int>();
                }

                mem[lastNumber].Add(i);
            }

            return lastNumber;
        }

        public long ComputePart2()
        {
            return Play(30000000);
        }
    }
}