using System;
using System.Collections.Generic;
using System.Linq;

namespace Event2020.Day08
{
    public class Day08
    {
        private readonly List<Instruction> _input;

        public Day08(string input)
        {
            _input = input.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => new Instruction(t.Trim())).ToList();
        }

        public int ComputePart1()
        {
            return new GameConsole(_input).Run();
        }

        public int ComputePart2()
        {
            var options = new List<List<Instruction>>();
            var allOptions = _input.Select(t => t).Where(t => t.Command == "nop" || t.Command == "jmp").ToList();

            for (int skip = 0; skip < allOptions.Count; skip++) // 4
            {
                var skipping = 0;
                var opt = new List<Instruction>(_input.Select(t => new Instruction(t.Command, t.Value)));
                for (int k = 0; k < _input.Count; k++)
                {
                    if (_input[k].Command == "nop")
                    {
                        if (skip == skipping)
                        {
                            opt[k] = new Instruction("jmp", _input[k].Value);
                            break;
                        }

                        skipping++;
                    }

                    if (_input[k].Command == "jmp")
                    {
                        if (skip == skipping)
                        {
                            opt[k] = new Instruction("nop", _input[k].Value);
                            break;
                        }

                        skipping++;
                    }
                }

                options.Add(opt);
            }

            foreach (var option in options)
            {
                var console = new GameConsole(option);
                var res = console.Run();
                if (console.Finished)
                {
                    return res;
                }
            }

            return 0;
        }
    }

    public class GameConsole
    {
        private int _pos;
        private int _acc;
        private readonly List<int> _posVisited;
        private readonly List<Instruction> _instructions;
        private bool _running;
        public bool Finished;

        public GameConsole(List<Instruction> instructions)
        {
            _instructions = instructions;
            _acc = 0;
            _posVisited = new List<int>();
        }

        public int Run()
        {
            _running = true;
            while (_running)
            {
                if (_posVisited.Contains(_pos))
                {
                    return _acc;
                }

                var newPos = 0;
                switch (_instructions[_pos].Command)
                {
                    case "nop":
                        newPos = _pos + 1;
                        break;
                    case "acc":
                        _acc += _instructions[_pos].Value;
                        newPos = _pos + 1;
                        break;
                    case "jmp":
                        newPos = _pos + _instructions[_pos].Value;
                        break;
                }

                _posVisited.Add(_pos);
                _pos = newPos;

                if (_pos >= _instructions.Count)
                {
                    Finished = true;
                    _running = false;
                }
            }

            return _acc;
        }
    }


    public class Instruction
    {
        public string Command { get; set; }
        public int Value { get; set; }

        public Instruction(string value)
        {
            var parts = value.Split(" ").Select(t => t.Trim()).ToArray();

            Command = parts[0];
            Value = int.Parse(parts[1]);
        }

        public Instruction(string command, int value)
        {
            Command = command;
            Value = value;
        }
    }
}