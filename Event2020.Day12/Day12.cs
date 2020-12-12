using System;
using System.Collections.Generic;
using System.Linq;
using Shared;

namespace Event2020.Day12
{
    public class Day12
    {
        private List<string> _input;
        private (string command, int amount)[] _commands;


        public Day12(string input)
        {
            _input = input.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim()).ToList();
            _commands = _input.Select(t => (t.Substring(0, 1), int.Parse(t.Substring(1)))).ToArray();
        }

        public Coordinate Move(Coordinate curPos, string command, int amount)
        {
            switch (command)
            {
                case "N":
                    curPos.Y -= amount;
                    break;
                case "S":
                    curPos.Y += amount;
                    break;

                case "E":
                    curPos.X += amount;
                    break;
                case "W":
                    curPos.X -= amount;
                    break;
            }

            return curPos;
        }

        public string TurnRight(string cur, int amount)
        {
            for (var i = 0; i < amount / 90; i++)
            {
                switch (cur)
                {
                    case "N":
                        cur = "E";
                        break;
                    case "E":
                        cur = "S";
                        break;
                    case "S":
                        cur = "W";
                        break;
                    case "W":
                        cur = "N";
                        break;
                }
            }

            return cur;
        }

        public string TurnLeft(string cur, int amount)
        {
            for (var i = 0; i < amount / 90; i++)
            {
                switch (cur)
                {
                    case "N":
                        cur = "W";
                        break;
                    case "E":
                        cur = "N";
                        break;
                    case "S":
                        cur = "E";
                        break;
                    case "W":
                        cur = "S";
                        break;
                }
            }

            return cur;
        }

        public Coordinate TurnWaypointRight(Coordinate pos, string cur, int amount)
        {
            for (var i = 0; i < amount / 90; i++)
            {
                pos = new Coordinate(pos.Y * -1, pos.X);
            }

            return pos;
        }

        public Coordinate TurnWaypointLeft(Coordinate pos, string cur, int amount)
        {
            for (var i = 0; i < amount / 90; i++)
            {
                pos = new Coordinate(pos.Y, pos.X*-1);
            }

            return pos;
        }

        public long ComputePart1()
        {
            var pos = new Coordinate(0, 0);
            var direction = "E";

            foreach (var command in _commands)
            {
                switch (command.command)
                {
                    case "N":
                    case "S":
                    case "E":
                    case "W":
                        pos = Move(pos, command.command, command.amount);
                        break;
                    case "L":
                        direction = TurnLeft(direction, command.amount);
                        break;
                    case "R":
                        direction = TurnRight(direction, command.amount);
                        break;
                    case "F":
                        pos = Move(pos, direction, command.amount);
                        break;
                }
            }

            return Math.Abs(pos.X) + Math.Abs(pos.Y);
        }

        public long ComputePart2()
        {
            var waypointPos = new Coordinate(10, -1);
            var shipPos = new Coordinate(0, 0);
            var shipDirection = "E";
            foreach (var command in _commands)
            {
                switch (command.command)
                {
                    case "N":
                    case "S":
                    case "E":
                    case "W":
                        waypointPos = Move(waypointPos, command.command, command.amount);
                        break;
                    case "L":
                        waypointPos = TurnWaypointLeft(waypointPos, shipDirection, command.amount);
                        shipDirection = TurnLeft(shipDirection, command.amount);
                        break;
                    case "R":
                        waypointPos = TurnWaypointRight(waypointPos, shipDirection, command.amount);
                        shipDirection = TurnRight(shipDirection, command.amount);
                        break;
                    case "F":
                        var moveX = waypointPos.X * command.amount;
                        var moveY = waypointPos.Y * command.amount;

                        shipPos.X += moveX;
                        shipPos.Y += moveY;
                        break;
                }
            }

            return Math.Abs(shipPos.X) + Math.Abs(shipPos.Y);
        }
    }
}