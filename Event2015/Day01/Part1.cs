using Shared;
using System.Linq;

namespace Event2015.Day01
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
            return _input.Count(t => t.Equals('(')) - _input.Count(t => t.Equals(')'));
        }
    }
}