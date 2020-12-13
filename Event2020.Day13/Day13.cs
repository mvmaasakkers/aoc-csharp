using System;
using System.Collections.Generic;
using System.Linq;

namespace Event2020.Day13
{
    public class Day13
    {
        private List<string> _input;

        private int timestamp;
        private List<string> _buses = new List<string>();


        public Day13(string input)
        {
            _input = input.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim()).ToList();
            timestamp = int.Parse(_input[0]);
            _buses = _input[1].Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public long ComputePart1()
        {
            var b = 0;
            var buses = new Dictionary<int, int>();
            var buslist = _buses.Where(t => t != "x").Select(t => int.Parse(t)).ToList();
            foreach (var bus in buslist)
            {
                buses[bus] = 0;
            }

            var newBUses = new Dictionary<int, int>();
            foreach (var bus in buses)
            {
                for (var i = timestamp; i < timestamp * 10; i++)
                {
                    var m = i % bus.Key;
                    if (m == 0)
                    {
                        newBUses[bus.Key] = i;
                        break;
                    }
                }
            }

            var closest = newBUses.Max(t => t.Value);

            foreach (var newBUse in newBUses)
            {
                if (newBUse.Value < closest)
                {
                    b = newBUse.Key;
                    closest = newBUse.Value;
                }
            }

            return closest - timestamp * b;
        }

        public long ComputePart2()
        {
            var buslist = _buses.Where(s => s != "x").Select(int.Parse).ToList();
            var busOff = new List<int>(buslist.Count);
            for (int i = 0; i < _buses.Count; i++)
            {
                if (_buses[i] != "x")
                {
                    busOff.Add(i % buslist[busOff.Count]);
                }
            }

            long time = 0;
            long add = buslist[0];
            int busI = 0;
            while (true)
            {
                bool found = true;
                for (int i = busI + 1; i < buslist.Count; i++)
                {
                    int bus = buslist[i];
                    if (Calc(bus, time) == busOff[i])
                    {
                        add *= bus;
                        busI = i;
                    }
                    else
                    {
                        found = false;
                        break;
                    }
                }

                if (found)
                {
                    break;
                }

                time += add;
            }

            return time;
        }

        private int Calc(int bus, long time)
        {
            int timeLeft = (int) (time % bus);
            if (timeLeft == 0)
            {
                return timeLeft;
            }

            return bus - timeLeft;
        }
    }
}