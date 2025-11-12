using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MunicipalServicesApp.Model;

namespace MunicipalServicesApp.Data
{
    public class RequestPriorityHeap
    {
        private readonly List<ServiceRequest> items = new List<ServiceRequest>();

        // Small helper methods for index calculations in a binary heap.
        private int Parent(int i) => (i - 1) / 2;
        private int Left(int i) => 2 * i + 1;
        private int Right(int i) => 2 * i + 2;

        public int Count => items.Count;

        // Insert a request into the heap
        public void Insert(ServiceRequest req) 
        {
            if (req == null) return;

            // Add to end and bubble up
            items.Add(req);
            int i = items.Count - 1;

            while (i > 0 && items[Parent(i)].Priority > items[i].Priority)  
            {
                // Swap current node with parent
                var tmp = items[i];
                items[i] = items[Parent(i)];
                items[Parent(i)] = tmp;
                i = Parent(i);
            }
        }

        // Remove and return the smallest-priority request (root of the heap)
        public ServiceRequest ExtractMin()
        {
            if (items.Count == 0) return null;
            if (items.Count == 1)
            {
                var only = items[0];
                items.RemoveAt(0);
                return only;
            }

            var root = items[0];
            items[0] = items[items.Count - 1];
            items.RemoveAt(items.Count - 1);
            Heapify(0);
            return root;
        }

        public ServiceRequest Peek() => items.Count > 0 ? items[0] : null; // Peak at smallest priority request.

        private void Heapify(int i)
        {
            int smallest = i;
            int l = Left(i);
            int r = Right(i);

            if (l < items.Count && items[l].Priority < items[smallest].Priority) smallest = l;
            if (r < items.Count && items[r].Priority < items[smallest].Priority) smallest = r;

            if (smallest != i)
            {
                var tmp = items[i];
                items[i] = items[smallest];
                items[smallest] = tmp;
                Heapify(smallest);
            }
        }

        public void Clear() => items.Clear();

    }
}
