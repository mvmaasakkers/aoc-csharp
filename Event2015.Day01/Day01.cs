using System.Linq;

namespace Event2015.Day01
{
    public class Day01
    {
        private string _input;

        public Day01(string input)
        {
            _input = input;
        }

        public int ComputePart1()
        {
            return _input.Count(t => t.Equals('(')) - _input.Count(t => t.Equals(')'));
            ;
        }

        public int ComputePart2()
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
                    return i + 1;
                }
            }


            return 0;
        }
    }
}