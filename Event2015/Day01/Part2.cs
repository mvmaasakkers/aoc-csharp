using System.Linq;
using Shared;

namespace Event2015.Day01
{
    public class Part2
    {
        private string _input;

        public Part2(string input)
        {
            _input = input;
        }

        public int Compute()
        {
            var currentPosition = 0;

            for (var i = 0; i <= _input.Length; i++)
            {
                switch (_input[i])
                {
                    case '(':
                        currentPosition++;
                        break;
                    case ')':
                        currentPosition--;
                        break;
                }

                if (currentPosition < 0)
                {
                    return i+1;
                }
            }


            return 0;
        }
    }
}