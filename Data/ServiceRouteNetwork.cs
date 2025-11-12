using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MunicipalServicesApp.Model;

namespace MunicipalServicesApp.Data

{
    internal class ServiceRouteNetwork
    {
        // adjacency list: node id -> list of (neighbor id, weight)
        private readonly Dictionary<string, List<(string neighbor, int weight)>> adjList = new();
        // map id -> original request so we can read category/location later
        private readonly Dictionary<string, ServiceRequest> requestMap = new();

        // Add a node to the graph. If it's already there, do nothing.
        public void AddVertex(ServiceRequest req)
        {
            if (req == null || string.IsNullOrWhiteSpace(req.Id)) return;
            if (!adjList.ContainsKey(req.Id))
            {
                adjList[req.Id] = new List<(string, int)>();
                requestMap[req.Id] = req;
            }
        }



        // Connect two nodes with an undirected edge (both ways).
        // weight should be positive, but we don't enforce here.
        public void AddEdge(string fromId, string toId, int weight)
        {
            if (string.IsNullOrWhiteSpace(fromId) || string.IsNullOrWhiteSpace(toId) || fromId == toId) return;
            if (!adjList.ContainsKey(fromId) || !adjList.ContainsKey(toId)) return;
            if (!adjList[fromId].Any(x => x.neighbor == toId))
                adjList[fromId].Add((toId, weight));
            if (!adjList[toId].Any(x => x.neighbor == fromId))
                adjList[toId].Add((fromId, weight));
        }

        // Calculate a simple weight between two requests.
        // Lower weight = more similar / easier to group.
        private int CalcWeight(ServiceRequest a, ServiceRequest b)
        {
            double weight = 10.0;  // start with a base number, then subtract if things match

            // category match
            if (!string.IsNullOrWhiteSpace(a.Category) && !string.IsNullOrWhiteSpace(b.Category)
                && string.Equals(a.Category, b.Category, StringComparison.OrdinalIgnoreCase))
                weight -= 3.0;

            // location match
            if (!string.IsNullOrWhiteSpace(a.Location) && !string.IsNullOrWhiteSpace(b.Location)
                && string.Equals(a.Location, b.Location, StringComparison.OrdinalIgnoreCase))
                weight -= 3.0;

            // similar date
            var days = Math.Abs((a.DateReported.Date - b.DateReported.Date).TotalDays);
            if (days <= 0.5) weight -= 2.0;
            else if (days <= 3) weight -= 1.0;

            // priority match
            if (a.Priority == b.Priority)
                weight -= 1.0;

            return (int)Math.Max(1, Math.Round(weight));
        }

        // Build the graph from a list of requests.
        // threshold: only connect pairs with weight <= threshold.
        public void BuildFromRequests(IEnumerable<ServiceRequest> reqs, double threshold = 10.0)
        {
            if (reqs == null) return;
            var list = reqs.ToList();
            if (list.Count == 0) return;

            // add every request as a node
            foreach (var r in list)
                AddVertex(r);

            // keep track of added pairs so we don't duplicate edges
            var addedPairs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    var a = list[i];
                    var b = list[j];
                    int w = CalcWeight(a, b);
                    if (w <= threshold)
                    {
                        string key = MakePairKey(a.Id, b.Id);
                        if (!addedPairs.Contains(key))
                        {
                            AddEdge(a.Id, b.Id, w);
                            addedPairs.Add(key);
                        }
                    }
                }
            }
        }

        private static string MakePairKey(string a, string b) // make a stable key for a pair
        {
            return string.Compare(a, b, StringComparison.OrdinalIgnoreCase) <= 0
                ? a + "||" + b
                : b + "||" + a;
        }

        // Return a simple Minimum Spanning Tree using a Prim-like approach.
        // We return the list of edges and the total cost.
        public (List<(string from, string to, int weight, string category)> edges, int totalCost) GetMinimumSpanningTree()
        {
            var resultEdges = new List<(string from, string to, int weight, string category)>();
            int totalCost = 0;
            if (adjList.Count == 0) return (resultEdges, 0);

            var visited = new HashSet<string>();
            var start = adjList.Keys.First();
            visited.Add(start);

            var pq = new List<(string from, string to, int weight)>();
            pq.AddRange(adjList[start].Select(x => (start, x.neighbor, x.weight)));

            while (pq.Count > 0)
            {
                var edge = pq.OrderBy(e => e.weight).First();
                pq.Remove(edge);

                if (visited.Contains(edge.to)) 
                    continue;

                visited.Add(edge.to);
               
                totalCost += edge.weight;

                // Find the category from either request
                string category = null;
                if (requestMap.TryGetValue(edge.from, out var fromReq))
                    category = fromReq.Category;
                else if (requestMap.TryGetValue(edge.to, out var toReq))
                    category = toReq.Category;

                resultEdges.Add((edge.from, edge.to, edge.weight, category ?? "Miscellaneous"));

                foreach (var next in adjList[edge.to])
                {
                    if (!visited.Contains(next.neighbor))
                        pq.Add((edge.to, next.neighbor, next.weight));
                }
            }

                var readableEdges = resultEdges.Select(e => (
                $"{requestMap[e.from].Category} — {requestMap[e.from].Location}",
                $"{requestMap[e.to].Category} — {requestMap[e.to].Location}",
                e.weight
                 )).ToList();

                return (resultEdges, totalCost);
            
        }
    }
}


