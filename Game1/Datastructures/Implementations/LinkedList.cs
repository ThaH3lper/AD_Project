using System;
using System.Collections;
using System.Collections.Generic;

namespace Game1.Datastructures.Implementations
{
    public class LinkedList<T> : ADT.IList<T>
    {
        /// <summary>
        /// Node class is a container for the stack data, and a pointer to the next node.
        /// </summary>
        private class Node
        {
            public Node Next { get; set; }

            public T Data { get; set; }

            public Node(T data, Node next)
            {
                this.Data = data;
                this.Next = next;
            }
        }


        private Node Head { get; set; }

        public int Count { get; set; }

        public bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public T this[int i]
        {
            get
            {
                return GetNodeAt(i).Data;
            }
            set
            {
                Node node = GetNodeAt(i);
                if (node != null) node.Data = value;
            }
        }

        public void Add(T item)
        {
            if (item == null) return;

            var newNode = new Node(item, Head);
            Head = newNode;
            ++Count;
        }

        public void AddRange(System.Collections.Generic.IEnumerable<T> items)
        {
            if (items == null) return;

            foreach (var item in items)
            {
                Add(item);
            }
        }

        private Node GetNodeAt(int i)
        {
            var node = Head;
            int index = Count - 1 ;
            while (node != null)
            {
                if (index == i)
                    return node;

                index--;
                node = node.Next;
            }

            return null;
        }

        public bool Contains(T item)
        {
            var node = Head;
            while (node != null)
            {
                if (node.Data.Equals(item))
                    return true;
                node = node.Next;
            }
            return false;
        }

        bool ICollection<T>.Remove(T item)
        {
            Node foundNode = null;
            Node prevNode = Head.Next;
            var node = Head;
            while (node != null)
            {
                if (node.Data.Equals(item))
                {
                    foundNode = node;
                    break;
                }
                prevNode = node;
                node = node.Next;
            }


            if (foundNode == null)
                return false;

            if (foundNode == Head)
            {
                Head = Head.Next;
            } else
            {
                prevNode.Next = foundNode.Next;
            }
            
            Count--;
            return true;
        }

        public void Clear()
        {
            Count = 0;
            Head = null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var node = Head;
            while (node != null)
            {
                yield return node.Data;
                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

       
    }

}
