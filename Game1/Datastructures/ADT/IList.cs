using System.Collections.Generic;
using Patrik.GameProject;

namespace Game1.Datastructures.ADT
{
    public interface IList<T> : IEnumerable<T>, ICollection<T>
    {
       //// int Count { get; set; }

        //void Add(T item);

        void AddRange(IEnumerable<T> items);

        //void AddRange(ICollection<Tile> tileColliders);

        T this[int key] { get; set; }

       

        //bool Contains(T item);

        //void Remove(T item);

        //void Clear();
    }
}
