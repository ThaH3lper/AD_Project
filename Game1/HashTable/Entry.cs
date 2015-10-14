/*
 * Patrik Nilsson, TGSPA
 * ae3210, DA404A
 */
namespace Hashtabell_Patrik_Nilsson
{
    /// <summary>
    /// Entry is a object in the hashtable. Contains a key and a value. Key and Value can be any type.
    /// </summary>
    /// <typeparam name="K">Type on the key.</typeparam>
    /// <typeparam name="T">Type on the value.</typeparam>
    class Entry<K, T>
    {
        /// <summary>
        /// Key to find this entry and get the value.
        /// </summary>
        public K key { get; private set; }

        /// <summary>
        /// Value of this entry.
        /// </summary>
        public T value { get; private set; }

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
            Entry<K, T> entry = (Entry<K, T>)obj;
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
