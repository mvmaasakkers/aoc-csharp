using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Event2015.Day07
{
    public class Input
    {
        public Input(string value)
        {
            var p = value.Split("->").Select(t => t.Trim()).ToArray();
            var rNumbers = new Regex("^[0-9]*$");
            var rOther = new Regex("^(?<l>[a-z]+) (?<op>AND|OR|LSHIFT|RSHIFT) (?<r>[a-z0-9]+)$");

            if (rNumbers.IsMatch(p[0]))
            {
                Op = "SET";
                Signal = int.Parse(p[0]);
            }
            else if (p[0].StartsWith("NOT "))
            {
                Op = "NOT";
                Wire = p[0].Replace("NOT ", "").Trim();
            }
            else
            {
                var m = rOther.Match(p[0]).Groups;
                if (int.TryParse(m["l"].Value.Trim(), out int ln))
                {
                    Ln = ln;
                    L = "";
                }
                else
                {
                    L = m["l"].Value.Trim();
                }
                
                if (int.TryParse(m["r"].Value.Trim(), out int rn))
                {
                    Rn = rn;
                    R = "";
                }
                else
                {
                    R = m["r"].Value.Trim();
                }

                Op = m["op"].Value.Trim();
            }

            Dest = p[1].Trim();

            // R = p[1];
        }

        public string Op { get; set; }
        public int Signal { get; set; }

        public string Wire { get; set; }
        public string L { get; set; }
        public int Ln { get; set; }
        public string R { get; set; }
        public int Rn { get; set; }
        public string Dest { get; set; }
    }

    public class Day
    {
        private List<Input> _input;
        private string[] _rawInput;

        public Day(string input)
        {
            _rawInput = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToArray();
            _input = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(t => new Input(t))
                .ToList();
        }

        public long Part1()
        {
            var wires = new Dictionary<string, int>();
            foreach (var input in _input)
            {
                if (input.Dest != null && !wires.ContainsKey(input.Dest))
                {
                    wires[input.Dest] = 0;
                }

                if (input.L != null && !wires.ContainsKey(input.L))
                {
                    wires[input.L] = 0;
                }

                if (input.R != null && !wires.ContainsKey(input.R))
                {
                    wires[input.R] = 0;
                }

                if (input.Wire != null && !wires.ContainsKey(input.Wire))
                {
                    wires[input.Wire] = 0;
                }


                switch (input.Op)
                {
                    case "SET":
                        wires[input.Dest] = input.Signal;
                        break;
                    case "AND":
                        var al = (input.L == "") ? input.Ln : wires[input.L];
                        var ar = (input.R == "") ? input.Rn : wires[input.R];
                        wires[input.Dest] = al & ar;
                        break;
                    case "OR":
                        var ol = (input.L == "") ? input.Ln : wires[input.L];
                        var or = (input.R == "") ? input.Rn : wires[input.R];

                        wires[input.Dest] = ol | or;
                        break;
                    case "LSHIFT":
                        var lsl = (input.L == "") ? input.Ln : wires[input.L];
                        var lsr = (input.R == "") ? input.Rn : wires[input.R];
                        wires[input.Dest] = lsl << lsr;
                        break;
                    case "RSHIFT":
                        var rsl = (input.L == "") ? input.Ln : wires[input.L];
                        var rsr = (input.R == "") ? input.Rn : wires[input.R];
                        wires[input.Dest] = rsl >> rsr;
                        break;
                    case "NOT":
                        wires[input.Dest] = (UInt16)(~wires[input.Wire]);
                        break;
                }
            }

            
            return wires["a"];
        }

        public long Part2()
        {
            return 0;
        }
    }
}