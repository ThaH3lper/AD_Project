/*
 * Patrik Nilsson, TGSPA
 * ae3210, DA404A
 */
using Game1.Datastructures.ADT;
using Game1.Datastructures.Implementations;
using System;
using System.Linq;

namespace Patrik.GameProject.Datastructures.Implementations
{
    /// <summary>
    /// Hashtable is containing all the methods and LinkedLists that the hashtable needs.
    /// </summary>
    /// <typeparam name="K">Type of the key.</typeparam>
    /// <typeparam name="T">Type of the value</typeparam>
    class Hashtable<K, T> : IMap<K, T>
    {
        /// <summary>
        /// Order of the values inserted to the hashtable. Doesn't remove insertions!
        /// </summary>
        private IList<T> insertionOrder = new LinkedList<T>();

        /// <summary>
        /// Table is an array with fixed size and contains LinkedLists.
        /// </summary>
        private IList<Entry>[] table;

        /// <summary>
        /// Returns the amount of entrys in insertionOrder.
        /// </summary>
        public int count { get { return insertionOrder.Count; } }

        public void Clear()
        {
            table = new LinkedList<Entry>[table.Count()];
            for (int i = 0; i < table.Count(); i++)
                table[i] = new LinkedList<Entry>();

            insertionOrder.Clear();
        }


        /// <summary>
        /// Constructor. Initializes all the lists for the array.
        /// </summary>
        /// <param name="size">The size of the hashtabelarray.</param>
        public Hashtable(int size = 10)
        {
            table = new LinkedList<Entry>[size];
            for (int i = 0; i < size; i++)
                table[i] = new LinkedList<Entry>();
        }

        /// <summary>
        /// Returns a index for the array mapped from the key.
        /// </summary>
        /// <param name="key">The key that will be mapped to a index.</param>
        /// <returns>Returns index</returns>
        private int HashIndex(K key)
        {
            int hashCode = key.GetHashCode();
            hashCode = hashCode % table.Length;
            return (hashCode < 0) ? -hashCode : hashCode;
        }

        /// <summary>
        /// Get the value of the key.
        /// </summary>
        /// <param name="key">key to find the entry.</param>
        /// <returns>returns the value of the key. If the entry isn't found, it will return default null.</returns>
        public T Get(K key)
        {
            int hashIndex = HashIndex(key);
            if (table[hashIndex].Contains(new Entry(key, default(T))))
            {
                var entry = table[hashIndex].SingleOrDefault(x => x.key.Equals(key));
                return entry.value;
            }
            return default(T);
        }

        public T this[K key]
        {
            get
            {
                return Get(key);
            }
            set
            {
                int hashIndex = HashIndex(key);
                if (table[hashIndex].Contains(new Entry(key, default(T))))
                {
                    var entry = table[hashIndex].SingleOrDefault(x => x.key.Equals(key));
                    if (entry != null)
                    {
                        entry.value = value;
                    }
                }
            }
        }


        /// <summary>
        /// Insert a new entry to the hashtable.
        /// </summary>
        /// <param name="key">The key top the entry.</param>
        /// <param name="value">The value of the entry.</param>
        public bool Put(K key, T value)
        {
            int hashIndex = HashIndex(key);
            if (table[hashIndex].Contains(new Entry(key, default(T))))
                return false;

            table[hashIndex].Add(new Entry(key, value));
            insertionOrder.Add(value);
            return true;
        }

        /// <summary>
        /// Removes the entry that has the key.
        /// </summary>
        /// <param name="key">The key to the entry we want to remove.</param>
        /// <returns>returns true if the entry was found and removed.</returns>
        public bool Remove(K key)
        {
            int hashIndex = HashIndex(key);
            if (table[hashIndex].Contains(new Entry(key, default(T))))
            {
                var entry = table[hashIndex].SingleOrDefault(x => x.key.Equals(key));

                // Remove it from the table
                table[hashIndex].Remove(entry);
                return true;
            }
            return false;
        }

        public bool Contains(K key)
        {
            int hashIndex = HashIndex(key);
            if (table[hashIndex].Contains(new Entry(key, default(T))))
            {
                var entry = table[hashIndex].SingleOrDefault(x => x.key.Equals(key));
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get the insertion order. The insertionorder can contain 
        /// values that are not longer existing in the hashtable.
        /// </summary>
        /// <returns>Returns a LinkedList of all values that have been inserted.</returns>
        public IList<T> GetInsertionOrder() { return insertionOrder; }

        /// <summary>
        /// Prints out the hashtable and how its currently structured.
        /// </summary>
        public void PrintTable()
        {
            for (int i = 0; i < table.Length; i++)
            {
                string s = i + "";
                foreach (Entry entry in table[i])
                    s += " -> " + entry.value;
                Console.WriteLine(s);
            }
        }

        /// <summary>
        /// Entry is a object in the hashtable. Contains a key and a value. Key and Value can be any type.
        /// </summary>
        /// <typeparam name="K">Type on the key.</typeparam>
        /// <typeparam name="T">Type on the value.</typeparam>
        private class Entry
        {
            /// <summary>
            /// Key to find this entry and get the value.
            /// </summary>
            public K key { get; private set; }

            /// <summary>
            /// Value of this entry.
            /// </summary>
            public T value { get; set; }

            /// <summary>
            /// Constructor to create Entry.
            /// </summary>
            /// <param name="key">The key of the entry</param>
            /// <param name="value">The value of the entry</param>
            public Entry(K key, T value)
            {
                this.key = key;
                this.value = value;
            }

            /// <summary>
            /// Overrides the "Equals" operator. Makes us able to use LinkedList.Find() method.
            /// </summary>
            /// <param name="obj">The entry to compaire to.</param>
            /// <returns>Return true if both keys are equal.</returns>
            public override bool Equals(object obj)
            {
                Entry entry = (Entry)obj;
                return key.Equals(entry.key);
            }

            /// <summary>
            /// Visual studio complains about not implementing this method if you override "Equals".
            /// </summary>
            /// <returns>Returns hashcode</returns>
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }
    }
}
