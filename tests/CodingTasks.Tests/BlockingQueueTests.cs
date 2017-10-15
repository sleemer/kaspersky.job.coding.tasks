using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CodingTasks.Tests
{
    public class BlockingQueueTests
    {
        [Fact]
        public void ShouldPushInstanceOfValueType()
        {
            // arrange
            var queue = new BlockingQueue<int>();

            // act
            // assert
            queue.Push(1);
        }

        [Fact]
        public void ShouldPopInstanceOfValueType()
        {
            // arrange
            var queue = new BlockingQueue<int>();
            queue.Push(1);

            // act
            var actual = queue.Pop();

            // assert
            Assert.Equal(1, actual);
        }

        [Fact]
        public void ShouldPushInstanceOfReferenceType()
        {
            // arrange
            var queue = new BlockingQueue<string>();

            // act
            // assert
            queue.Push("test");
        }

        [Fact]
        public void ShouldPopInstanceOfReferenceType()
        {
            // arrange
            var queue = new BlockingQueue<string>();
            queue.Push("test");

            // act
            var actual = queue.Pop();

            // assert
            Assert.Equal("test", actual);
        }

        [Fact]
        public void ShouldPushNull()
        {
            // arrange
            var queue = new BlockingQueue<string>();

            // act
            // assert
            queue.Push(null);
        }

        [Fact]
        public void ShouldPopNull()
        {
            // arrange
            var queue = new BlockingQueue<string>();
            queue.Push(null);

            // act
            var actual = queue.Pop();

            // assert
            Assert.Null(actual);
        }

        [Fact]
        public void ShouldBlock_WhenPopFromEmptyQueue()
        {
            // arrange
            var queue = new BlockingQueue<int>();
            Task pop;
            Task timeout;

            // act
            var actual = Task.WhenAny(
                pop = Task.Run(() => queue.Pop()),
                timeout = Task.Delay(TimeSpan.FromSeconds(1))).GetAwaiter().GetResult();

            // assert
            Assert.NotSame(pop, actual);
            Assert.Same(timeout, actual);
        }

        [Fact]
        public void ShouldUnBlock_WhenNewItemWasPushedToTheQueue()
        {
            // arrange
            var queue = new BlockingQueue<int>();
            Task<int> pop;
            Task pushWithTimeout;

            // act
            Task.WaitAll(
                pop = Task.Run(() => queue.Pop()),
                pushWithTimeout = Task.Delay(TimeSpan.FromSeconds(1)).ContinueWith(t => queue.Push(1)));

            // assert
            Assert.Equal(1, pop.Result);
        }
    }
}