using System.Collections.Generic;
using Shared;
using System.Linq;

namespace Event2015.Day03
{
    public class Part1
    {
        private string _input;

        public Part1(string input)
        {
            _input = input;
        }

        public int Compute()
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
    }
}