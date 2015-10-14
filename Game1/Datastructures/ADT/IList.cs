using System.Collections.Generic;

namespace Game1.Datastructures.ADT
{
    interface IList<T> : IEnumerable<T>
    {
        int Count { get; set; }

        void Add(T item);

        void AddRange(IEnumerable<T> items);

        T this[int key] { get; set; }

        bool Contains(T item);

        void Remove(T item);

        void Clear();
    }
}
