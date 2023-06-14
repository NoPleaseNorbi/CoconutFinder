using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{
    public class Node
    {
        private const int Radius = 25;
        public readonly Font NodeNumberFont = new Font(FontFamily.GenericSansSerif, 8f);
        public readonly Brush NodeNumberBrush = Brushes.Black;
        public readonly StringFormat NodeNumberFormat = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        public Point Location { get; }
        public int Number { get; }
        public List<Edge> Edges { get; }

        public Node(Point location, int number)
        {
            Location = location;
            Number = number;
            Edges = new List<Edge>();
        }

        public void AddEdge(Node target, int weight)
        {
            Edges.Add(new Edge(target, weight));
        }

        public bool ContainsPoint(Point point)
        {
            int dx = point.X - Location.X;
            int dy = point.Y - Location.Y;
            return (dx * dx) + (dy * dy) <= Radius * Radius;
        }

        public void Draw(Graphics graphics)
        {
            int x = Location.X - Radius;
            int y = Location.Y - Radius;
            int diameter = Radius * 2;
            graphics.FillEllipse(Brushes.Aqua, x, y, diameter, diameter);
            graphics.DrawString(Number.ToString(), NodeNumberFont, NodeNumberBrush, Location, NodeNumberFormat);
            foreach (Edge edge in Edges)
            {
                Point midPoint = new Point((Location.X + edge.Target.Location.X) / 2, (Location.Y + edge.Target.Location.Y) / 2);
                graphics.DrawString(edge.Weight.ToString(), NodeNumberFont, NodeNumberBrush, midPoint, NodeNumberFormat);
            }

        }
    }
}
