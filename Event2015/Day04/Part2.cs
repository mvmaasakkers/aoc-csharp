using Shared;
using System.Linq;

namespace Event2015.Day04
{
    public class Part2
    {
        private string _input;

        public Part2(string input)
        {
            _input = input;
        }

        public long Compute()
        {
            return FindHash.Find(_input, 6);
        }
    }
}