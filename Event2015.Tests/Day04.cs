using System.Collections.Generic;
using Event2015.Day04;
using Xunit;

namespace Event2015.Tests
{
    public class Day04
    {
        [Fact]
        public void Part1Test()
        {
            var tests = new Dictionary<string, long>
            {
                {"abcdef", 609043},
                {"pqrstuv", 1048970},
                {Data.RawInput, 346386}
            };
            
            foreach (var test in tests)
            {
                Assert.Equal(test.Value, new Part1(test.Key).Compute());                
            }
        }
        
        [Fact]
        public void Part2Test()
        {
            var tests = new Dictionary<string, long>
            {
                {Data.RawInput, 9958218}
            };
            
            foreach (var test in tests)
            {
                Assert.Equal(test.Value, new Part2(test.Key).Compute());                
            }
        }
    }
}