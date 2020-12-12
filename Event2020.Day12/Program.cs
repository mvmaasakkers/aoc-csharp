using System;
using System.IO;
using System.Linq;

namespace Event2020.Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllText("Input/part1.txt");
            var today = new Day12(data);
            
            Console.Write("Part 1: ");
            Console.WriteLine(today.ComputePart1());
            Console.Write("Part 2: ");
            Console.WriteLine(today.ComputePart2());
        }
    }
}