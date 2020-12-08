using System.Collections.Generic;
using Shared;
using System.Linq;

namespace Event2015.Day03
{
    public class Part2
    {
        private string _input;
        private string _santa;
        private string _roboSanta;


        public Part2(string input)
        {
            _input = input;
        }

        public int Compute()
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