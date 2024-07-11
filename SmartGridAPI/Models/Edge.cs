// Models/Edge.cs
namespace SmartGridAPI.Models
{
    public class Edge
    {
        public Node Node1 { get; set; }
        public Node Node2 { get; set; }
        public double Weight { get; set; }

        public Edge(Node node1, Node node2, double weight)
        {
            Node1 = node1;
            Node2 = node2;
            Weight = weight;
        }
    }
}
