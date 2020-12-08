using System.Collections.Generic;
using Event2015.Day07;
using Xunit;

namespace Event2015.Tests
{
    public class Day07
    {
        [Fact]
        public void Part1Test()
        {
            var tests = new Dictionary<string, long>
            {
                {Data.RawInput, -1}
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
                {Data.RawInput, -1}
            };
            
            foreach (var test in tests)
            {
                Assert.Equal(test.Value, new Day(test.Key).Part2());                
            }
        }
    }
}