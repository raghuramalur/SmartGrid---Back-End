// Models/Node.cs
namespace SmartGridAPI.Models
{
    public class Node
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Type { get; set; }
        public double Capacity { get; set; }
        public double Demand { get; set; }
        public double MaxLoad { get; set; }
    }
}