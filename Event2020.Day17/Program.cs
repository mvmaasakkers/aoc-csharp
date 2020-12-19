using System;
using System.IO;
using System.Linq;

namespace Event2020.Day17
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllText("Input/part1test.txt");
            var today = new Day17(data);
            
            Console.Write("Part 1: ");
            Console.WriteLine(today.ComputePart1());
            Console.Write("Part 2: ");
            Console.WriteLine(today.ComputePart2());
        }
    }
}