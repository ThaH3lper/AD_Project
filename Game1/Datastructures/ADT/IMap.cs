using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1.Datastructures.ADT
{
    interface IMap<K, T>
    {
        bool Put(K key, T value);

        T Get(K key);

        T this[K key] { get; set; }

        bool Remove(K key);

        bool Contains(K key);

        void Clear();
    }
}
