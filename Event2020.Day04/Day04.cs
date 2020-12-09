using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Event2020.Day04
{
    public class Day04
    {
        private List<string> _input;

        public Day04(string input)
        {
            _input = input.Split(new[] {"\r\n\r\n", "\n\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim()).ToList();
        }

        public int ComputePart1()
        {
            return _input.Count(_isBasicallyValid);
        }

        private bool _isBasicallyValid(string t)
        {
            return t.Contains("byr:") &&
                   t.Contains("iyr:") &&
                   t.Contains("eyr:") &&
                   t.Contains("hgt:") &&
                   t.Contains("hcl:") &&
                   t.Contains("ecl:") &&
                   t.Contains("pid:");
        }

        private bool _isComplexValid(string t)
        {
            var parts = t.Split(new string[] {"\r\n", "\n", " "}, StringSplitOptions.None)
                    .Select(c => c.Split(":"))
                    .ToDictionary(x => x[0].Trim(), x => x[1].Trim())
                ;

            var byr = parts["byr"];
            var iyr = parts["iyr"];
            var eyr = parts["eyr"];
            var hgt = parts["hgt"];
            var hcl = parts["hcl"];
            var ecl = parts["ecl"];
            var pid = parts["pid"];

            if (int.Parse(byr) < 1920 || int.Parse(byr) > 2002)
            {
                return false;
            }

            if (int.Parse(iyr) < 2010 || int.Parse(iyr) > 2020)
            {
                return false;
            }

            if (int.Parse(eyr) < 2020 || int.Parse(eyr) > 2030)
            {
                return false;
            }


            var rHeight = new Regex(@"^(?<height>\d.+?)(?<type>cm|in)$");
            if (!rHeight.IsMatch(hgt))
            {
                return false;
            }

            var height = int.Parse(rHeight.Match(hgt).Groups["height"].Value);
            var type = rHeight.Match(hgt).Groups["type"].Value;
            if (type == "cm")
            {
                if (height < 150 || height > 193)
                {
                    return false;
                }
            }

            if (type == "in")
            {
                if (height < 59 || height > 76)
                {
                    return false;
                }
            }

            if (!new Regex("^#[a-f0-9]{6}$").IsMatch(hcl))
            {
                return false;
            }

            if (ecl != "amb" && ecl != "blu" && ecl != "brn" && ecl != "gry" && ecl != "grn" && ecl != "hzl" &&
                ecl != "oth")
            {
                return false;
            }

            if (!new Regex("^[0-9]{9}$").IsMatch(pid))
            {
                return false;
            }

            return true;
        }

        public int ComputePart2()
        {
            return _input.Count(t =>
                _isBasicallyValid(t) && _isComplexValid(t)
            );
        }
    }
}