using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace Event2020.Day14
{
    public class Day14
    {
        private List<string> _input;
        private Regex r = new Regex(@"mem\[(?<addr>\d+)\] = (?<value>\d+)");

        public Day14(string input)
        {
            _input = input.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim()).ToList();
        }

        public long ComputePart1()
        {
            var mask = "";
            var memory = new Dictionary<int, string>();
            foreach (var l in _input)
            {
                if (l.StartsWith("mask"))
                {
                    mask = l;
                }
                else if (l.StartsWith("mem"))
                {
                    var memvalGroups = r.Match(l).Groups;
                    var addr = int.Parse(memvalGroups["addr"].Value);
                    var value = long.Parse(memvalGroups["value"].Value);

                    var bitmask = Convert.ToString(value, 2).PadLeft(mask.Length, '0').ToArray();

                    for (int i = 0; i < mask.Length; i++)
                    {
                        switch (mask[i])
                        {
                            case 'X':
                                continue;
                            case '1':
                                bitmask[i] = '1';
                                break;
                            case '0':
                                bitmask[i] = '0';
                                break;
                        }
                    }

                    memory[addr] = new string(bitmask);
                }
            }

            return memory.Sum(t => Convert.ToInt64(t.Value, 2));
        }

        public long ComputePart2()
        {
            var mask = "";
            var memory = new Dictionary<long, string>();
            foreach (var l in _input)
            {
                if (l.StartsWith("mask"))
                {
                    mask = l;
                }
                else if (l.StartsWith("mem"))
                {
                    var memvalGroups = r.Match(l).Groups;
                    var addr = long.Parse(memvalGroups["addr"].Value);
                    var value = long.Parse(memvalGroups["value"].Value);

                    var bitmask = Convert.ToString(value, 2).PadLeft(mask.Length, '0').ToArray();
                    var memBitmask = Convert.ToString(addr, 2).PadLeft(mask.Length, '0').ToArray();

                    for (int i = 0; i < mask.Length; i++)
                    {
                        switch (mask[i])
                        {
                            case 'X':
                                memBitmask[i] = 'X';
                                break;
                            case '1':
                                memBitmask[i] = '1';
                                break;
                            case '0':
                                continue;
                        }
                    }

                    GetMasks(new string(memBitmask)).ForEach(t =>
                    {
                        memory[Convert.ToInt64(t, 2)] = new string(bitmask);
                    });
                }
            }

            return memory.Sum(t => Convert.ToInt64(t.Value, 2));
        }

        private static List<string> GetMasks(string address)
        {
            if (!address.Any(c => c.Equals('X')))
            {
                return new List<string> {address};
            }

            var mask1 = address;
            var mask2 = address;
            var pos = address.IndexOf("X");
            if (pos >= 0)
            {
                mask1 = mask1.Remove(pos, 1).Insert(pos, "0");
                mask2 = mask2.Remove(pos, 1).Insert(pos, "1");
            }

            return GetMasks(mask1).Concat(GetMasks(mask2)).ToList();
        }
    }
}