﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DB_OPI.Queues
{
    class FixedSizedQueue<T> : ConcurrentQueue<T>
    {
        //private readonly object syncObject = new object();

        public int Size { get; private set; }

        public FixedSizedQueue(int size)
        {
            Size = size;
        }

        public new void Enqueue(T obj)
        {
            base.Enqueue(obj);
            while (base.Count > Size)
            {
                T outObj;
                base.TryDequeue(out outObj);
            }
            
        }
    }
}
