using System;
using System.Collections.Generic;
using System.Linq;

namespace Event2015.Day03
{
    public class Day03
    {
        private string _input;
        private string _santa;
        private string _roboSanta;

        public Day03(string input)
        {
            _input = input;
        }

        public int ComputePart1()
        {
            var currentLocation = new Location(0, 0);
            var locations = new List<string> {currentLocation.ToString()};

            foreach (var c in _input)
            {
                var nextLocation = move(c, new Location(currentLocation.X, currentLocation.Y));

                locations.Add(nextLocation.ToString());

                currentLocation = nextLocation;
            }

            return locations.Distinct().Count();
        }

        private Location move(char c, Location loc)
        {
            var nextLocation = new Location(loc.X, loc.Y);

            switch (c)
            {
                case '^':
                    nextLocation.Y -= 1;
                    break;
                case '>':
                    nextLocation.X += 1;
                    break;
                case '<':
                    nextLocation.X -= 1;
                    break;
                case 'v':
                    nextLocation.Y += 1;
                    break;
            }

            return nextLocation;
        }
        
        public int ComputePart2()
        {
            for (int i = 1; i <= _input.Length; i++)
            {
                if (i % 2 == 0)
                {
                    _roboSanta += _input[i - 1];
                }
                else
                {
                    _santa += _input[i - 1];
                }
            }

            var houses = travel(_santa);
            houses.AddRange(travel(_roboSanta));
            
            return houses.Distinct().Count();
        }
        
        private List<string> travel(string input)
        {
            var currentLocation = new Location(0, 0);
            var locations = new List<string> {currentLocation.ToString()};

            foreach (var c in input)
            {
                var nextLocation = move(c, new Location(currentLocation.X, currentLocation.Y));

                locations.Add(nextLocation.ToString());

                currentLocation = nextLocation;
            }

            return locations;
        }
    }
    
    public class Location
    {
        public override string ToString()
        {
            return $"y-{Y}-x-{X}";
        }

        public Location()
        {
        }

        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}