using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServicesApp.Data
{
    public class SinglyLinkedList<T> : IEnumerable<T>
    {
        private Node head = null;
        private Node tail = null;
        private int count = 0;

        private class Node
        {
            public T Value;
            public Node Next;
            public Node(T value) { Value = value; Next = null; }
        }

        public void AddLast(T value)
        {
            var n = new Node(value);
            if (head == null) head = n;
            else tail.Next = n;
            tail = n;
            count++;
        }

        public int Count => count;

        public IEnumerator<T> GetEnumerator()
        {
            var current = head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        // Optional: find by predicate
        public T Find(Predicate<T> predicate)
        {
            var current = head;
            while (current != null)
            {
                if (predicate(current.Value)) return current.Value;
                current = current.Next;
            }
            return default;
        }
    }
}
