using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using SmartGridAPI.Models;

namespace SmartGridAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NodesController : ControllerBase
    {
        [HttpPost("optimize")]
        public ActionResult<IEnumerable<Edge>> Optimize([FromBody] List<Node> nodes)
        {
            // Implement optimization logic here (e.g., MST, shortest path)
            var optimizedResult = OptimizeGrid(nodes);

            return Ok(optimizedResult);
        }

        private IEnumerable<Edge> OptimizeGrid(List<Node> nodes)
        {
            var edges = CreateAllEdges(nodes);
            var mst = Kruskal(edges, nodes.Count);
            return mst;
        }

        private List<Edge> CreateAllEdges(List<Node> nodes)
        {
            var edges = new List<Edge>();
            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = i + 1; j < nodes.Count; j++)
                {
                    var distance = CalculateDistance(nodes[i], nodes[j]);
                    edges.Add(new Edge(nodes[i], nodes[j], distance));
                }
            }
            return edges;
        }

        private double CalculateDistance(Node node1, Node node2)
        {
            var d1 = node1.Latitude - node2.Latitude;
            var d2 = node1.Longitude - node2.Longitude;
            return Math.Sqrt(d1 * d1 + d2 * d2);
        }

        private List<Edge> Kruskal(List<Edge> edges, int nodeCount)
        {
            edges.Sort((e1, e2) => e1.Weight.CompareTo(e2.Weight));
            var parent = new int[nodeCount];
            var rank = new int[nodeCount];
            for (int i = 0; i < nodeCount; i++)
            {
                parent[i] = i;
                rank[i] = 0;
            }

            var mst = new List<Edge>();
            foreach (var edge in edges)
            {
                var root1 = Find(edge.Node1.Id, parent);
                var root2 = Find(edge.Node2.Id, parent);

                if (root1 != root2)
                {
                    mst.Add(edge);
                    Union(root1, root2, parent, rank);
                }
            }
            return mst;
        }

        private int Find(int node, int[] parent)
        {
            if (parent[node] != node)
                parent[node] = Find(parent[node], parent);
            return parent[node];
        }

        private void Union(int root1, int root2, int[] parent, int[] rank)
        {
            if (rank[root1] > rank[root2])
                parent[root2] = root1;
            else if (rank[root1] < rank[root2])
                parent[root1] = root2;
            else
            {
                parent[root2] = root1;
                rank[root1]++;
            }
        }
    }
}
