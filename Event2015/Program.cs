using System;

namespace Event2015
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var command = input.Split()[0];
            var argument1 = input.Split()[1];
            
            Console.WriteLine(command);
            Console.WriteLine(argument1);
        }
    }
}
