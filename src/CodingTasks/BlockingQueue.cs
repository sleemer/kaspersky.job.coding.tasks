using System;
using System.Collections.Generic;
using System.Threading;

namespace CodingTasks
{
    /// <summary>
    /// A thread-safe blocking Queue of generic items.
    /// </summary>
    /// <typeparam name="T">The type of the item in the queue</typeparam>
    public sealed class BlockingQueue<T> : IDisposable
    {
        private readonly Queue<T> _queue = new Queue<T>();
        private readonly object _barier = new object();                                              // Used to sync access to the queue from concurrent threads
        private readonly ManualResetEventSlim _newObjectAvailable = new ManualResetEventSlim(false); // Used to awake threads that are blocked on Pop

        /// <summary>
        /// Adds item to the tail of the queue.
        /// </summary>
        /// <param name="item">The item to be added to the queue. The value can be a null reference.</param>
        public void Push(T item)
        {
            lock (_barier) {
                _queue.Enqueue(item);
                _newObjectAvailable.Set();
            }
        }

        /// <summary>
        /// Removes and returns the item at the head of the queue.
        /// </summary>
        /// <returns>The item removed from the queue.</returns>
        /// <remarks>
        /// If the queue's empty, this method blocks calling thread until a new item is added to the queue.
        /// </remarks>
        public T Pop()
        {
            while (true) {
                lock (_barier) {
                    if (_queue.Count > 0) return _queue.Dequeue();
                    _newObjectAvailable.Reset();
                }
                _newObjectAvailable.Wait();
            }
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            _newObjectAvailable.Dispose();
        }

        #endregion
    }
}