using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingTasks
{
    public static class Algorithm
    {
        private static IEnumerable<Tuple<int, int>> Empty = new Tuple<int, int>[0];

        /// <summary>
        /// An algorithm to find out all posible pairs of numbers with the defined sum.
        /// </summary>
        /// <param name="numbers">A collection of numbers.</param>
        /// <param name="sum">A value that is used to find pair of numbers a,b such as a+b=sum in <paramref name="numbers"/>.</param>
        /// <returns>A <see cref="IEnumerable{Tuple{int,int}}"/> to enumerate through pairs of numbers.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the collection of numbers is null.</exception>
        /// <remarks>Each number in collection can participate only in one pair.<remarks>
        public static IEnumerable<Tuple<int, int>> GetPairsOfNumbersWithSum(IEnumerable<int> numbers, int sum)
        {
            if (numbers == null) throw new ArgumentNullException(nameof(numbers));

            var sortedNumbers = numbers.ToArray();
            if (sortedNumbers.Length < 2) return Empty;
            Array.Sort(sortedNumbers);

            return GetPairsOfNumbersWithSumImpl(sortedNumbers, sum);
        }

        private static IEnumerable<Tuple<int, int>> GetPairsOfNumbersWithSumImpl(int[] sortedNumbers, int sum)
        {
            int left = 0;
            int right = sortedNumbers.Length - 1;
            while (left < right) {
                switch (sortedNumbers[left] + sortedNumbers[right]) {
                    case int result when (result > sum):
                        right--;
                        break;
                    case int result when (result < sum):
                        left++;
                        break;
                    default:
                        yield return Tuple.Create(sortedNumbers[left++], sortedNumbers[right--]);
                        break;
                }
            }
        }
    }
}
