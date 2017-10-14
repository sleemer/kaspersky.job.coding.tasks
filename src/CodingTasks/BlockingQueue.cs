using System;
using System.Collections.Generic;
using System.Threading;

namespace CodingTasks
{
    // A thread-safe blocking Queue of generic itmes.
    public sealed class BlockingQueue<T>
    {
        private readonly Queue<T> _queue = new Queue<T>();
        private readonly object _barier = new object();                                              // Used to sync access to the queue from concurrent threads
        private readonly ManualResetEventSlim _newObjectAvailable = new ManualResetEventSlim(false); // Used to awake threads that are blocked on Pop

        // Adds item to the tail of the queue.
        public void Push(T item)
        {
            lock (_barier) {
                _queue.Enqueue(item);
                _newObjectAvailable.Set();
            }
        }

        // Removes and returns the item at the head of the queue. 
        // If the queue's empty, this method blocks calling thread until new item added to the queue.  
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