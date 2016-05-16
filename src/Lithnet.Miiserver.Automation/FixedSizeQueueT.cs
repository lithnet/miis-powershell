using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Concurrent;

namespace Lithnet.Miiserver.Automation
{
    public class FixedSizedQueue<T> : ConcurrentQueue<T>
    {
        private readonly object syncObject = new object();

        public int Size { get; private set; }

        public FixedSizedQueue(int size)
        {
            this.Size = size;
        }

        public new void Enqueue(T obj)
        {
            base.Enqueue(obj);
            lock (this.syncObject)
            {
                while (this.Count > this.Size)
                {
                    T outObj;
                    this.TryDequeue(out outObj);
                }
            }
        }
    }
}
