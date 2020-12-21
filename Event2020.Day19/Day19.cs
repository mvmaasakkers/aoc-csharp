using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Event2020.Day19
{
    public class Day19
    {
        private List<string> _input;
        private string _rawInput;

        public Day19(string input)
        {
            _input = input.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim()).ToList();
            _rawInput = input;
        }

        public long ComputePart1()
        {

            Regex literalRegex = new Regex("^\"(\\w+)\"");

            Rule MakeRule(string s)
            {
                Match literalM = literalRegex.Match(s);
                if (literalM.Success)
                {
                    return new Rule(literalM.Groups[1].Value);
                }
                else
                {
                    return new Rule(s.Split("|").Select(p => p.Split(" ").Where(s => !String.IsNullOrWhiteSpace(s))));
                }
            }

            var input = File.ReadAllText("Input/part1.txt")
                .Split("\n\n")
                .ToArray();
            Rule.Rules = input[0]
                .Split("\n")
                .Select(l => l.Split(": "))
                .ToDictionary(p => p[0], p => MakeRule(p[1]));

            Console.WriteLine(
                input[1]
                    .Split("\n")
                    .Sum(l => Rule.Rules["0"].Check(l) == l.Length ? 1 : 0)
            );

            return 0;
        }

        public long ComputePart2()
        {

            Regex literalRegex = new Regex("^\"(\\w+)\"");

            Rule2 MakeRule(string s)
            {
                Match literalM = literalRegex.Match(s);
                if (literalM.Success)
                {
                    return new Rule2(literalM.Groups[1].Value);
                }
                else
                {
                    return new Rule2(s.Split("|").Select(p => p.Split(" ").Where(s => !String.IsNullOrWhiteSpace(s))));
                }
            }

            var input = _rawInput
                .Split("\n\n")
                .ToArray();
            Rule2.Rules = input[0]
                .Split("\n")
                .Select(l => l.Split(": "))
                .ToDictionary(p => p[0], p => MakeRule(p[1]));

            Rule2.Rules["0"] = new Rule2((s, i) =>
            {
                i = Rule2.Rules["42"].Check(s, i);
                if (i != -1)
                {
                    i = Rule2.Rules["42"].Check(s, i);
                    if (i != -1)
                    {
                        int matches = 0;
                        while (true)
                        {
                            var retI = Rule2.Rules["42"].Check(s, i);
                            if (retI != -1)
                            {
                                i = retI;
                                matches++;
                            }
                            else
                            {
                                break;
                            }
                        }

                        i = Rule2.Rules["31"].Check(s, i);
                        if (i != -1)
                        {
                            while (matches > 0)
                            {
                                var retI = Rule2.Rules["31"].Check(s, i);
                                if (retI != -1)
                                {
                                    i = retI;
                                    matches--;
                                }
                                else
                                {
                                    break;
                                }
                            }

                            return i;
                        }
                    }
                }

                return -1;
            });
            
            return input[1]
                .Split("\n")
                .Sum(l => Rule2.Rules["0"].Check(l) == l.Length ? 1 : 0);
        }


        class Rule
        {
            public static Dictionary<string, Rule> Rules;

            string Literal;
            IEnumerable<IEnumerable<string>> Alts;

            public Rule(string literal) => Literal = literal;
            public Rule(IEnumerable<IEnumerable<string>> alts) => Alts = alts;

            public int Check(string s, int index = 0)
            {
                if (Literal is null)
                {
                    foreach (var alt in Alts)
                    {
                        bool match = true;
                        int localIndex = index;
                        foreach (var rule in alt)
                        {
                            int retI = Rules[rule].Check(s, localIndex);
                            if (retI != -1)
                            {
                                localIndex = retI;
                            }
                            else
                            {
                                match = false;
                                break;
                            }
                        }

                        if (match)
                        {
                            return localIndex;
                        }
                    }

                    return -1;
                }
                else
                {
                    return s.Substring(index).StartsWith(Literal) ? index + Literal.Length : -1;
                }
            }
        }


        class Rule2
        {
            public static Dictionary<string, Rule2> Rules;

            string Literal;
            IEnumerable<IEnumerable<string>> Alts;
            Func<string, int, int> Func;

            public Rule2(string literal) => Literal = literal;
            public Rule2(IEnumerable<IEnumerable<string>> alts) => Alts = alts;
            public Rule2(Func<string, int, int> func) => Func = func;

            public int Check(string s, int index = 0)
            {
                if (Literal is null && Func is null)
                {
                    foreach (var alt in Alts)
                    {
                        bool match = true;
                        int localIndex = index;
                        foreach (var rule in alt)
                        {
                            int retI = Rules[rule].Check(s, localIndex);
                            if (retI != -1)
                            {
                                localIndex = retI;
                            }
                            else
                            {
                                match = false;
                                break;
                            }
                        }

                        if (match)
                        {
                            return localIndex;
                        }
                    }

                    return -1;
                }
                else if (Literal is null)
                {
                    return Func(s, index);
                }
                else
                {
                    return s.Substring(index).StartsWith(Literal) ? index + Literal.Length : -1;
                }
            }
        }
    }
}