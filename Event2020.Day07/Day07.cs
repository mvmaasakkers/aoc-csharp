using System;
using System.Collections.Generic;
using System.Linq;

namespace Event2020.Day07
{
    public class Day07
    {
        private List<string> _input;

        public Day07(string input)
        {
            _input = input.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim()).ToList();
        }

        public long ComputePart1()
        {
            return getBags()["shiny gold"].ParentNames().Distinct().Select(t => t).Count();
        }

        public long ComputePart2()
        {
            return getBags()["shiny gold"].BagCount();
        }

        private Dictionary<string, Bag> getBags()
        {
            var bags = new Dictionary<string, Bag>();

            foreach (var line in _input)
            {
                var split1 = line.Split("bags contain");
                var color = split1[0].Trim();
                var containsColors = split1[1].Split(",").Select(x => x.Trim());

                var bag = bags.GetValueOrDefault(color) ?? new Bag {ColorName = color};
                if (!bags.ContainsKey(color))
                {
                    bags[color] = bag;
                }

                foreach (var c in containsColors)
                {
                    if (c == "no other bags.")
                    {
                        continue;
                    }

                    var ams = c.Split()[0];
                    var count = int.Parse(ams);
                    var containsColor = c.Replace(ams, "").Split("bag")[0].Trim();

                    var otherBag = bags.GetValueOrDefault(containsColor) ?? new Bag {ColorName = containsColor};
                    if (!bags.ContainsKey(containsColor))
                    {
                        bags[containsColor] = otherBag;
                    }

                    otherBag.Parents.Add(bag);
                    bag.Children.Add((otherBag, count));
                }
            }

            return bags;
        }
    }

    public class Bag
    {
        public string ColorName { get; set; }
        public List<(Bag, int)> Children { get; set; } = new List<(Bag, int)>();
        public List<Bag> Parents { get; set; } = new List<Bag>();

        public List<string> ParentNames() =>
            Parents.SelectMany(p => p.ParentNames().Concat(new[] {p.ColorName})).ToList();

        public long BagCount() => Children.Sum(c => c.Item2) + Children.Sum(c => c.Item1.BagCount() * c.Item2);
    }
}