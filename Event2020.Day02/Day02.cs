using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Event2020.Day02
{
    public class Day02
    {
        private List<Input> _input;

        public Day02(string input)
        {
            Regex r = new Regex(@"(?<min>[0-9]*)\-(?<max>[0-9]*) (?<char>[a-zA-Z]*): (?<password>[0-9a-zA-Z]*)");

            _input = input.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => new Input(r.Match(t).Groups)).ToList();
        }

        public long ComputePart1()
        {
            return _input.Select(t => t).Where(t =>
                t.Password.Count(c => c == t.Char) >= t.Min &&
                t.Password.Count(c => c == t.Char) <= t.Max).ToList().Count;
        }

        public long ComputePart2()
        {
            return _input.Select(t => t)
                .Where(c => c.Password[c.Min - 1] == c.Char ^ c.Password[c.Max - 1] == c.Char).ToList().Count;
        }
    }

    public class Input
    {
        public Input(GroupCollection groups)
        {
            Min = int.Parse(groups["min"].Value);
            Max = int.Parse(groups["max"].Value);
            Char = char.Parse(groups["char"].Value);
            Password = groups["password"].Value;
        }

        public int Min { get; set; }
        public int Max { get; set; }
        public char Char { get; set; }
        public string Password { get; set; }
    }
}