using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Event2020.Day16
{
    public class Input
    {
        public string Key;

        public int Range1Start;
        public int Range1End;
        public int Range2Start;
        public int Range2End;
    }

    public class Day16
    {
        private List<string> _input;
        private string _rawInput;
        private List<Input> ranges = new List<Input>();

        public Day16(string input)
        {
            _rawInput = input;
            _input = input.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim()).ToList();
        }

        private Regex matchLine = new
            Regex(@"(?<key>[a-z ]+): (?<r1start>[\d]+)-(?<r1end>[\d]+) or (?<r2start>[\d]+)-(?<r2end>[\d]+)");

        public long ComputePart1()
        {
            ranges = new List<Input>();
            foreach (var i in _input)
            {
                var m = matchLine.Match(i);
                if (m.Success)
                {
                    ranges.Add(new Input
                    {
                        Key = m.Groups["key"].Value,
                        Range1Start = int.Parse(m.Groups["r1start"].Value),
                        Range1End = int.Parse(m.Groups["r1end"].Value),
                        Range2Start = int.Parse(m.Groups["r2start"].Value),
                        Range2End = int.Parse(m.Groups["r2end"].Value),
                    });
                }
            }

            var parts = _rawInput.Split(new[] {"\r\n\r\n", "\n\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim()).ToList();

            var yourTicket = parts[1].Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries).Skip(1)
                .Select(t => t.Split(",").Select(t => int.Parse(t))).ToList();
            var nearbyTickets = parts[2].Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries).Skip(1)
                .Select(t => t.Split(",").Select(t => int.Parse(t))).ToList();


            var invalid = 0;
            foreach (var nearbyTicket in nearbyTickets)
            {
                foreach (var n in nearbyTicket)
                {
                    var t = ranges.Where(t =>
                        n >= t.Range1Start && n <= t.Range1End || n >= t.Range2Start && n <= t.Range2End);
                    if (t.Count() == 0)
                    {
                        invalid += n;
                    }
                }
            }

            return invalid;
        }

        public long ComputePart2()
        {
            ranges = new List<Input>();
            foreach (var i in _input)
            {
                var m = matchLine.Match(i);
                if (m.Success)
                {
                    ranges.Add(new Input
                    {
                        Key = m.Groups["key"].Value,
                        Range1Start = int.Parse(m.Groups["r1start"].Value),
                        Range1End = int.Parse(m.Groups["r1end"].Value),
                        Range2Start = int.Parse(m.Groups["r2start"].Value),
                        Range2End = int.Parse(m.Groups["r2end"].Value),
                    });
                }
            }

            var parts = _rawInput.Split(new[] {"\r\n\r\n", "\n\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim()).ToList();

            var yourTicket = parts[1].Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries).Skip(1)
                .Select(t => t.Split(",").Select(t => int.Parse(t))).ToList();
            var nearbyTickets = parts[2].Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries).Skip(1)
                .Select(t => t.Split(",").Select(t => int.Parse(t))).ToList();


            var validTickets = new List<IEnumerable<int>>();
            foreach (var nearbyTicket in nearbyTickets)
            {
                var invalid = 0;
                foreach (var n in nearbyTicket)
                {
                    var t = ranges.Where(t =>
                        n >= t.Range1Start && n <= t.Range1End || n >= t.Range2Start && n <= t.Range2End);
                    if (t.Count() == 0)
                    {
                        invalid += n;
                    }
                }

                if (invalid == 0)
                {
                    validTickets.Add(nearbyTicket);
                }
            }

            return 0;
        }

        // public bool IsTicketValid(IEnumerable<int> ticket)
        // {
        //     var invalid = 0;
        //     foreach (var n in ticket)
        //     {
        //         var t = ranges.Where(t =>
        //             n >= t.Range1Start && n <= t.Range1End || n >= t.Range2Start && n <= t.Range2End);
        //         if (t.Count() > 0)
        //         {
        //             invalid += n;
        //         }
        //     }
        // }
    }
}