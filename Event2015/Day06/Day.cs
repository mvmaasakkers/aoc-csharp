﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Event2015.Day06
{
    public class Day
    {
        private List<Input> _input;

        public Day(string input)
        {
            _input = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).Select(t => new Input(t))
                .ToList();
        }

        public long Part1()
        {
            var lights = new Dictionary<string, int>();

            foreach (var input in _input)
            {
                for (int y = input.From.Y; y <= input.To.Y; y++)
                {
                    for (int x = input.From.X; x <= input.To.X; x++)
                    {
                        var coordinate = new Coordinate(x, y);
                        if (!lights.TryGetValue(coordinate.ToString(), out _))
                        {
                            lights[coordinate.ToString()] = 0;
                        }

                        switch (input.Command)
                        {
                            case "turn off":
                                lights[coordinate.ToString()] = 0;
                                break;
                            case "turn on":
                                lights[coordinate.ToString()] = 1;
                                break;
                            case "toggle":
                                lights[coordinate.ToString()] = (lights[coordinate.ToString()] == 0) ? 1 : 0;
                                break;
                        }
                    }
                }
            }

            return lights.Count(t => t.Value == 1);
        }

        public long Part2()
        {
            var lights = new Dictionary<string, int>();

            foreach (var input in _input)
            {
                for (int y = input.From.Y; y <= input.To.Y; y++)
                {
                    for (int x = input.From.X; x <= input.To.X; x++)
                    {
                        var coordinate = new Coordinate(x, y);
                        if (!lights.TryGetValue(coordinate.ToString(), out _))
                        {
                            lights[coordinate.ToString()] = 0;
                        }

                        switch (input.Command)
                        {
                            case "turn off":
                                if (lights[coordinate.ToString()] > 0)
                                {
                                    lights[coordinate.ToString()]--;
                                }

                                break;
                            case "turn on":
                                lights[coordinate.ToString()]++;
                                break;
                            case "toggle":
                                lights[coordinate.ToString()] += 2;
                                break;
                        }
                    }
                }
            }
            return lights.Sum(t => t.Value);
        }
    }

    public struct Input
    {
        public Input(string value)
        {
            var r = new Regex("^([a-z ]+)([0-9]+),([0-9]+)[a-z ]+([0-9]+),([0-9]+)$");
            var matches = r.Match(value).Groups;
            Command = matches[1].Value.Trim();
            From = new Coordinate(int.Parse(matches[2].Value), int.Parse(matches[3].Value));
            To = new Coordinate(int.Parse(matches[4].Value), int.Parse(matches[5].Value));
        }

        public string Command { get; set; }
        public Coordinate From { get; set; }
        public Coordinate To { get; set; }
    }

    public struct Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"y-{Y}-x-{X}";
        }
    }
}