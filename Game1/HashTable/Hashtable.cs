/*
 * Patrik Nilsson, TGSPA
 * ae3210, DA404A
 */
using System;
using System.Collections.Generic;

namespace Hashtabell_Patrik_Nilsson
{
    /// <summary>
    /// Hashtable is containing all the methods and LinkedLists that the hashtable needs.
    /// </summary>
    /// <typeparam name="K">Type of the key.</typeparam>
    /// <typeparam name="T">Type of the value</typeparam>
    class Hashtable<K, T>
    {
        /// <summary>
        /// Order of the values inserted to the hashtable. Doesn't remove insertions!
        /// </summary>
        private LinkedList<T> insertionOrder = new LinkedList<T>();

        /// <summary>
        /// Table is an array with fixed size and contains LinkedLists.
        /// </summary>
        private LinkedList<Entry<K, T>>[] table;

        /// <summary>
        /// Returns the amount of entrys in insertionOrder.
        /// </summary>
        public int count { get { return insertionOrder.Count; } }
      

        /// <summary>
        /// Constructor. Initializes all the lists for the array.
        /// </summary>
        /// <param name="size">The size of the hashtabelarray.</param>
        public Hashtable(int size)
        {
            table = new LinkedList<Entry<K, T>>[size];
            for (int i = 0; i < size; i++)
                table[i] = new LinkedList<Entry<K, T>>();
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
            if(table[hashIndex].Contains(new Entry<K, T>(key, default(T))))
            {
                Entry<K, T> entry = table[hashIndex].Find(new Entry<K, T>(key, default(T))).Value;
                return entry.value;
            }
            return default(T);
        }

        /// <summary>
        /// Insert a new entry to the hashtable.
        /// </summary>
        /// <param name="key">The key top the entry.</param>
        /// <param name="value">The value of the entry.</param>
        public bool Put(K key, T value)
        {
            int hashIndex = HashIndex(key);
            if (table[hashIndex].Contains(new Entry<K, T>(key, default(T))))
                return false;
            table[hashIndex].AddLast(new Entry<K, T>(key, value));
            insertionOrder.AddLast(value);
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
            if (table[hashIndex].Contains(new Entry<K, T>(key, default(T))))
            {
                Entry<K, T> entry = table[hashIndex].Find(new Entry<K, T>(key, default(T))).Value;
                insertionOrder.Remove(entry.value);
                table[hashIndex].Remove(entry);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get the insertion order. The insertionorder can contain 
        /// values that are not longer existing in the hashtable.
        /// </summary>
        /// <returns>Returns a LinkedList of all values that have been inserted.</returns>
        public LinkedList<T> GetInsertionOrder(){ return insertionOrder; }

        /// <summary>
        /// Prints out the hashtable and how its currently structured.
        /// </summary>
        public void PrintTable()
        {
            for (int i = 0; i < table.Length; i++)
            {
                string s = i + "";
                foreach(Entry<K, T> entry in table[i])
                    s += " -> " + entry.value;
                Console.WriteLine(s);
            }
        }
    }
}
