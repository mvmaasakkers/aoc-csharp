using System.Collections.Generic;
using Event2015.Day05;
using Xunit;

namespace Event2015.Tests
{
    public class Day05
    {
        [Fact]
        public void Part1Test()
        {
            var tests = new Dictionary<string, long>
            {
                {Data.RawInput, 238}
            };
            
            foreach (var test in tests)
            {
                Assert.Equal(test.Value, new Day(test.Key).Part1());                
            }
        }
        
        [Fact]
        public void Part2Test()
        {
            var tests = new Dictionary<string, long>
            {
                {Data.RawInput, 69}
            };
            
            foreach (var test in tests)
            {
                Assert.Equal(test.Value, new Day(test.Key).Part2());                
            }
        }
    }
}