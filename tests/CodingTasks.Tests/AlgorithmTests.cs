using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CodingTasks.Tests
{
    public class GetPairsOfNumbersWithSumUnitTests
    {
        [Fact]
        public void ShouldThrow_When_NumbersAreEmpty()
        {
            // arrange
            // act
            // assert
            Assert.Throws<ArgumentNullException>(() => Algorithm.GetPairsOfNumbersWithSum(null, 0));
        }

        [Theory]
        [InlineData(new[] { 1, 2, 1, 1, 0 }, 2, "(0,2) (1,1)")]
        [InlineData(new[] { 1, 2, 1, 7, 1, 0, 6, 3 }, 9, "(2,7) (3,6)")]
        [InlineData(new[] { 11, 2, 1, 7, 1, 0, 6, 3, 5 }, 11, "(0,11) (5,6)")]
        public void ShouldFindPairsOfNumbersWithSum(int[] numbers, int sum, string strExpected)
        {
            // arrange
            var expected = CreateArrayOfTuplesFromString(strExpected);

            // act
            var actual = Algorithm.GetPairsOfNumbersWithSum(numbers, sum).ToArray();

            // assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(new int[] { }, 2)]
        [InlineData(new[] { 1, 2, 1, 7, 1, 0, 6, 3 }, 100)]
        public void ShouldNotFindPairsOfNumbersWithSum(int[] numbers, int sum)
        {
            // arrange
            // act
            var actual = Algorithm.GetPairsOfNumbersWithSum(numbers, sum).ToArray();

            // assert
            Assert.Empty(actual);
        }

        // Parses a string and creates an array of tuples of two integers.
        // Example of an input string: "(0,2) (1,1)"
        private static Tuple<int, int>[] CreateArrayOfTuplesFromString(string str)
            => str.Split(new[] { " " }, StringSplitOptions.None)
                  .Select(pair => pair.Split(new[] { ",", "(", ")" }, StringSplitOptions.RemoveEmptyEntries))
                  .Select(pair => Tuple.Create(int.Parse(pair[0]), int.Parse(pair[1])))
                  .ToArray();
    }
}
