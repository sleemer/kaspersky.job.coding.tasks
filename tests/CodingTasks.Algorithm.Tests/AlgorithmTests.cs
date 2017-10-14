using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CodingTasks.Algorithm.Tests
{
    public class GetPairsOfNumbersWithSumUnitTests
    {
        [Fact]
        public void ShouldThrow_When_NumersAreEmpty()
        {
            // arrange
            // act
            // assert
            Assert.Throws<ArgumentNullException>(() => Algorithms.GetPairsOfNumbersWithSum(null, 0));
        }

        [Theory]
        [InlineData(new[] { 1, 2, 1, 1, 0 }, 2, "0,2;1,1")]
        [InlineData(new[] { 1, 2, 1, 7, 1, 0, 6, 3 }, 9, "2,7;3,6")]
        [InlineData(new[] { 11, 2, 1, 7, 1, 0, 6, 3, 5 }, 11, "0,11;5,6")]
        public void ShouldFindPairsOfNumbersWithSum(int[] numbers, int sum, string strExpected)
        {
            // arrange
            var expected = CreateArrayOfTuplesFromString(strExpected);

            // act
            var actual = Algorithms.GetPairsOfNumbersWithSum(numbers, sum).ToArray();

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
            var actual = Algorithms.GetPairsOfNumbersWithSum(numbers, sum).ToArray();

            // assert
            Assert.Empty(actual);
        }

        private static Tuple<int, int>[] CreateArrayOfTuplesFromString(string str)
            => str.Split(new[] { ";" }, StringSplitOptions.None)
                  .Select(pair => pair.Split(new[] { "," }, StringSplitOptions.None))
                  .Select(pair => Tuple.Create(int.Parse(pair[0]), int.Parse(pair[1])))
                  .ToArray();
    }
}
