using System;
using System.Collections.Generic;
using System.Threading;

namespace CodingTasks
{
    /// <summary>
    /// A thread-safe blocking Queue of generic itmes.
    /// </summary>
    /// <typeparam name="T">The type of the item in the queue</typeparam>
    public sealed class BlockingQueue<T>
    {
        private readonly Queue<T> _queue = new Queue<T>();
        private readonly object _barier = new object();                                              // Used to sync access to the queue from concurrent threads
        private readonly ManualResetEventSlim _newObjectAvailable = new ManualResetEventSlim(false); // Used to awake threads that are blocked on Pop

        /// <summary>
        /// Adds item to the tail of the queue.
        /// </summary>
        /// <typeparam name="T">The type of the item to push to the queue</typeparam>
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
        /// <typeparam name="T">The type of the item to pop from the queue</typeparam>
        /// <remarks>
        /// If the queue's empty, this method blocks calling thread until new item added to the queue.  
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
    }
}