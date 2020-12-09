using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Event2015.Day08
{
    public class Day08
    {
        private List<Input> _input;

        public Day08(string input)
        {
            _input = input.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => new Input(t, Regex.Unescape(t), Regex.Escape(t))).ToList();
        }

        public long ComputePart1()
        {
            return _input.Sum(d => d.Original.Length - d.Escaped.Length + 2);
        }

        public long ComputePart2()
        {
            return _input.Sum(d => d.Unescaped.Length + d.Unescaped.Count(c => c == '\"') - d.Original.Length + 2);
        }
    }
    
    public class Input
    {
        public Input(string original, string escaped, string unescaped)
        {
            Original = original;
            Escaped = escaped;
            Unescaped = unescaped;
        }

        public string Original { get; set; }
        public string Escaped { get; set; }
        public string Unescaped { get; set; }
    }
}