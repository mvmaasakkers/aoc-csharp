using System.Collections.Generic;
using Event2015.Day06;
using Xunit;

namespace Event2015.Tests
{
    public class Day06
    {
        [Fact]
        public void Part1Test()
        {
            var tests = new Dictionary<string, long>
            {
                {Data.RawInput, 569999}
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
                {Data.RawInput, 17836115}
            };

            foreach (var test in tests)
            {
                Assert.Equal(test.Value, new Day(test.Key).Part2());
            }
        }
    }
}