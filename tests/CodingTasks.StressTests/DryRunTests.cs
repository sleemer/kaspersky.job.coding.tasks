using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CodingTasks.StressTests
{
    public class DryRunTests
    {
        [Fact]
        public void BlockingQueue_OnePublisherOneReader()
        {
            // arrange
            var dequeuedValues = new ConcurrentDictionary<int, bool>(); // Used to check  blocking queue
            var queue = new BlockingQueue<int>();

            // act
            Parallel.Invoke(
                () => Publish(queue, 0, 100_000),
                () => Consume(queue, 100_000, dequeuedValues));

            // assert
            Assert.Equal(100_000, dequeuedValues.Count);
        }

        [Fact]
        public void BlockingQueue_OnePublisherTwoReaders()
        {
            // arrange
            var dequeuedValues = new ConcurrentDictionary<int, bool>(); // Used to check  blocking queue
            var queue = new BlockingQueue<int>();

            // act
            Parallel.Invoke(
                () => Publish(queue, 0, 200_000),
                () => Consume(queue, 100_000, dequeuedValues),
                () => Consume(queue, 100_000, dequeuedValues));

            // assert
            Assert.Equal(200_000, dequeuedValues.Count);
        }

        [Fact]
        public void BlockingQueue_TwoPublishersTwoReaders()
        {
            // arrange
            var dequeuedValues = new ConcurrentDictionary<int, bool>(); // Used to check  blocking queue
            var queue = new BlockingQueue<int>();

            // act
            Parallel.Invoke(
                () => Publish(queue, 0, 100_000),
                () => Publish(queue, 100_000, 200_000),
                () => Consume(queue, 100_000, dequeuedValues),
                () => Consume(queue, 100_000, dequeuedValues));

            // assert
            Assert.Equal(200_000, dequeuedValues.Count);
        }

        [Fact]
        public void BlockingQueue_TwoPublishersFourReaders()
        {
            // arrange
            var dequeuedValues = new ConcurrentDictionary<int, bool>(); // Used to check  blocking queue
            var queue = new BlockingQueue<int>();

            // act
            Parallel.Invoke(
                () => Publish(queue, 0, 2_000_000),
                () => Publish(queue, 2_000_000, 4_000_000),
                () => Consume(queue, 1_000_000, dequeuedValues),
                () => Consume(queue, 1_000_000, dequeuedValues),
                () => Consume(queue, 1_000_000, dequeuedValues),
                () => Consume(queue, 1_000_000, dequeuedValues));

            // assert
            Assert.Equal(4_000_000, dequeuedValues.Count);
        }

        private static void Publish(BlockingQueue<int> queue, int start, int count)
            => Enumerable.Range(start, count).ToList().ForEach(queue.Push);

        private static void Consume(BlockingQueue<int> queue, int count, ConcurrentDictionary<int, bool> dequeuedValues)
            => Enumerable.Range(0, count).ToList().ForEach(_ => dequeuedValues[queue.Pop()] = true);
    }
}
