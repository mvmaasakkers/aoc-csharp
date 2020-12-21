using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Event2020.Day21
{
    //Part 1: Part 1: 1679
    //lmxt,rggkbpj,mxf,gpxmf,nmtzlj,dlkxsxg,fvqg,dxzq

    public class Day21
    {
        private List<string> _input;

        public Day21(string input)
        {
            _input = input.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim()).ToList();
        }

        Regex regex = new Regex(@"(?<ingredients>.*) \(contains (?<contains>.*)\)",
            RegexOptions.Compiled | RegexOptions.ECMAScript);

        public long ComputePart1()
        {
            var ingredients = new List<string>();
            var allergens = new List<string>();
            var ac = new Dictionary<string, int>();
            var listing = new Dictionary<string, Dictionary<string, int>>();

            foreach (var item in _input)
            {
                var match = regex.Match(item);
                if (match.Success)
                {
                    var ingredientPart = match.Groups["ingredients"].Value
                        .Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                    var allergenPart = match.Groups["contains"].Value
                        .Split(new string[] {", "}, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var a in allergenPart)
                    {
                        foreach (var i in ingredientPart)
                        {
                            if (!listing.ContainsKey(i)) listing[i] = new Dictionary<string, int>();
                            if (!listing[i].ContainsKey(a)) listing[i][a] = 0;
                            listing[i][a]++;
                        }

                        if (!ac.ContainsKey(a)) ac[a] = 0;
                        ac[a]++;
                    }

                    ingredients.AddRange(ingredientPart);
                    allergens.AddRange(allergenPart);
                }
            }

            var ingAll = new Dictionary<string, string>();
            while (true)
            {
                var single = listing.Where(item => item.Value.Count(ok => ok.Value == ac[ok.Key]) == 1);
                if (!single.Any())
                {
                    break;
                }

                foreach (var x in single)
                {
                    var a = x.Value.First(item => item.Value == ac[item.Key]).Key;
                    ingAll[x.Key] = a;
                    foreach (var k in listing)
                    {
                        k.Value[a] = 0;
                    }
                }
            }

            var count = 0;
            foreach (var item in ingredients)
            {
                if (!ingAll.ContainsKey(item)) count++;
            }

            return count;
        }

        public string ComputePart2()
        {
            var ingredients = new List<string>();
            var allergens = new List<string>();
            var ac = new Dictionary<string, int>();
            var listing = new Dictionary<string, Dictionary<string, int>>();

            foreach (var item in _input)
            {
                var match = regex.Match(item);
                if (match.Success)
                {
                    var ingredientPart = match.Groups["ingredients"].Value
                        .Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                    var allergenPart = match.Groups["contains"].Value
                        .Split(new string[] {", "}, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var a in allergenPart)
                    {
                        foreach (var i in ingredientPart)
                        {
                            if (!listing.ContainsKey(i)) listing[i] = new Dictionary<string, int>();
                            if (!listing[i].ContainsKey(a)) listing[i][a] = 0;
                            listing[i][a]++;
                        }

                        if (!ac.ContainsKey(a)) ac[a] = 0;
                        ac[a]++;
                    }

                    ingredients.AddRange(ingredientPart);
                    allergens.AddRange(allergenPart);
                }
            }

            var ingAll = new Dictionary<string, string>();
            while (true)
            {
                var single = listing.Where(item => item.Value.Count(ok => ok.Value == ac[ok.Key]) == 1);
                if (!single.Any())
                {
                    break;
                }

                foreach (var x in single)
                {
                    var a = x.Value.First(item => item.Value == ac[item.Key]).Key;
                    ingAll[x.Key] = a;
                    foreach (var k in listing)
                    {
                        k.Value[a] = 0;
                    }
                }
            }

            var danger = "";
            foreach (var item in ingAll.OrderBy(item => item.Value))
            {
                danger += item.Key + ",";
            }

            danger = danger.Substring(0, danger.Length - 1);

            return danger;
        }
    }
}