using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Event2015.Day05
{
    public class Day
    {
        private List<string> _input;

        public Day(string input)
        {
            _input = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
        
        private List<string> doubleLetters = "abcdefghijklmnopqrstuvwxyz".Select(t => new string(t, 2)).ToList();
        private List<Regex> betweenLetters = "abcdefghijklmnopqrstuvwxyz".Select(t => new Regex(t + "[a-z]{1}"+t)).ToList();
        private Regex vowels = new Regex("[aeiou]");

        public long Part1()
        {
            return _input
                    .Select(t => t)
                    .Where(t => vowels.Matches(t).Count >= 3)
                    .Where(t => doubleLetters.Count(t.Contains) > 0)
                    .Count(t =>
                        !t.Contains("ab") &&
                        !t.Contains("cd") &&
                        !t.Contains("pq") &&
                        !t.Contains("xy")
                    );

        }

        public long Part2()
        {
            return  _input.Select(t => t).Where(Has2).Count(HasBetween);
        }
        
        
        private bool HasBetween(string value) {
            for (int i = 0; i < value.Length - 2; i++) {
                if (value[i] == value[i + 2]) {
                    return true;
                }
            }
            return false;
        }

        private bool Has2(string value) {
            for (int i = 0; i < value.Length - 3; i++) {
                for (int ii = i + 2; ii < value.Length - 1; ii++) {
                    if (new string(value.ToCharArray(), i, 2) == new string(value.ToCharArray(), ii, 2)) {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}