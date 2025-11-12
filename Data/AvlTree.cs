using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MunicipalServicesApp.Model;

namespace MunicipalServicesApp.Data
{
    public class AvlTree
    {
        private class Node
        {
            public ServiceRequest Data;
            public Node Left;
            public Node Right;
            public int Height;

            public Node(ServiceRequest data)
            {
                Data = data;
                Height = 1;
            }
        }

        private Node root;

        //----------------------------------------------------------------helper methods--------------------------------------------------//
        private int Height(Node n) => n?.Height ?? 0;

        private int BalanceFactor(Node n) => (n == null) ? 0 : Height(n.Left) - Height(n.Right);


        private Node RotateRight(Node y)
        {
            var x = y.Left;
            var t2 = x.Right;

            x.Right = y;
            y.Left = t2;

            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;
            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;

            return x;


        }

        private Node RotateLeft(Node x)
        {
            var y = x.Right;
            var t2 = y.Left;

            y.Left = x;
            x.Right = t2;

            x.Height = Math.Max(Height(x.Left), Height(x.Right)) + 1;
            y.Height = Math.Max(Height(y.Left), Height(y.Right)) + 1;

            return y;

        }

        private int Compare(ServiceRequest a, ServiceRequest b)
        {
            int c = a.DateReported.CompareTo(b.DateReported);
            if (c != 0) return c;
            return string.Compare(a.Id, b.Id, StringComparison.Ordinal);
        }

        //----------------------------------------------------public API------------------------------------------------//

        // Insert a request into the AVL tree
        public void Insert(ServiceRequest item)
        {
            if (item == null) return;
            root = InsertInternal(root, item);
        }

        // Search by Id (returns matching ServiceRequest or null)
        public ServiceRequest SearchById(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;
            return SearchByIdInternal(root, id);
        }

        // Traverse in-order (oldest -> newest)
        public void InOrderTraverse(Action<ServiceRequest> visit)
        {
            InOrderInternal(root, visit);
        }

        // Get most recent N requests (newest first)
        public List<ServiceRequest> GetMostRecent(int n)
        {
            var result = new List<ServiceRequest>();
            if (n <= 0) return result;
            GetMostRecentInternal(root, result, n);
            return result;
        }

        //---------------------------------------------Internal implementation--------------------------------------------//

        private Node InsertInternal(Node node, ServiceRequest item)
        {
            if (node == null) 
                return new Node(item);

            if (Compare(item, node.Data) < 0)
                node.Left = InsertInternal(node.Left, item);
            else
                node.Right = InsertInternal(node.Right, item);

            // update height
            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));

            // get balance
            int balance = BalanceFactor(node);

            // If unbalanced, there are 4 cases:

            // Left Left Case
            if (balance > 1 && Compare(item, node.Left.Data) < 0)
                return RotateRight(node);

            // Right Right Case
            if (balance < -1 && Compare(item, node.Right.Data) > 0)
                return RotateLeft(node);

            // Left Right Case
            if (balance > 1 && Compare(item, node.Left.Data) > 0)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            // Right Left Case
            if (balance < -1 && Compare(item, node.Right.Data) < 0)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            // balanced
            return node;
        }

        private ServiceRequest SearchByIdInternal(Node node, string id)
        {
            if (node == null) return null;
            if (node.Data.Id == id) return node.Data;

            var left = SearchByIdInternal(node.Left, id);
            if (left != null) return left;
            return SearchByIdInternal(node.Right, id);
        }

        private void InOrderInternal(Node node, Action<ServiceRequest> visit)
        {
            if (node == null) return;
            InOrderInternal(node.Left, visit);
            visit?.Invoke(node.Data);
            InOrderInternal(node.Right, visit);
        }

        private void GetMostRecentInternal(Node node, List<ServiceRequest> acc, int n)
        {
            if (node == null || acc.Count >= n) return;
            GetMostRecentInternal(node.Right, acc, n);
            if (acc.Count < n) acc.Add(node.Data);
            GetMostRecentInternal(node.Left, acc, n);
        }

    }


}    
