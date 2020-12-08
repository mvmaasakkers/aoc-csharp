using System.Collections.Generic;
using Event2015.Day03;
using Xunit;

namespace Event2015.Tests
{
    public class Day03
    {
        [Fact]
        public void Part1Test()
        {
            var tests = new Dictionary<string, long>
            {
                {">", 2},
                {"^>v<", 4},
                {"^v^v^v^v^v", 2},
                {Data.RawInput, 2572}
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
                {"^v", 3},
                {"^>v<", 3},
                {"^v^v^v^v^v", 11},
                {Data.RawInput, 2631}
            };
            
            foreach (var test in tests)
            {
                Assert.Equal(test.Value, new Part2(test.Key).Compute());                
            }
        }
    }
}
