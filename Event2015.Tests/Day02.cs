using System.Collections.Generic;
using Event2015.Day02;
using Xunit;


namespace Event2015.Tests
{
    public class Day02
    {
        
        [Fact]
        public void Part1Test()
        {
            var tests = new Dictionary<string, long>
            {
                {"2x3x4", 58},
                {"1x1x10", 43},
                {Day.InputRaw, 1598415}
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
                {"2x3x4", 34},
                {"1x1x10", 14},
                {Day.InputRaw, 3812909}
            };
            
            foreach (var test in tests)
            {
                Assert.Equal(test.Value, new Part2(test.Key).Compute());                
            }
        }
    }
}