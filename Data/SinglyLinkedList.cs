using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalServicesApp.Data
{
    public class SinglyLinkedList<T> : IEnumerable<T> // Simple singly linked list implementation
    {
        private Node head = null; // First node
        private Node tail = null; // Last node
        private int count = 0; // Number of elements

        private class Node // Node class
        {
            public T Value; // Value of the node
            public Node Next; // Pointer to the next node
            public Node(T value) // Constructor
            { 
                Value = value; Next = null;
            }
        }

        public void AddLast(T value) // Add value to the end of the list
        {
            var n = new Node(value);
            if (head == null) head = n;
            else tail.Next = n;
            tail = n;
            count++;
        }

        public int Count => count; // Get number of elements

        public IEnumerator<T> GetEnumerator() // Enumerator for iteration
        {
            var current = head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator(); // Non-generic enumerator

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
