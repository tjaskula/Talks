using System.Collections.Generic;
using ObjectOriented.Domain;
using Xunit;
using Xunit.Extensions;

namespace ObjectOriented.Tests.Domain
{
    public class WordCounterTests
    {
        [Fact]
        public void ShouldReturnZeroWhenNullText()
        {
            var wordCounter = new WordCounter();

            Assert.Equal(0, wordCounter.Count(null));
        }

        [Fact]
        public void ShouldReturnZeroWhenEmptyText()
        {
            var wordCounter = new WordCounter();

            Assert.Equal(0, wordCounter.Count(null));
        }

        [Theory, PropertyData("GetText")]
        public void ShouldReturnCountWhenText(string text, int expectedCount)
        {
            var wordCounter = new WordCounter();

            Assert.Equal(expectedCount, wordCounter.Count(text));
        }

        public static IEnumerable<object[]> GetText
        {
            get
            {
                return new[]
                {
                    new object[] {"One", 1},
                    new object[] {"One two", 2},
                    new object[] {"One two three four five six seven eight nine ten", 10}
                };
            }
        }
    }
}