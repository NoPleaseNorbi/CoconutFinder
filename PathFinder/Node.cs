using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{
    /// <summary>
    /// The class representing the nodes in the weighted graph
    /// </summary>
    public class Node
    {
        private const int Radius = 15;
        public readonly Font node_number_font = new Font(FontFamily.GenericSansSerif, 8f);
        public readonly Brush node_number_brush = Brushes.Black;
        public readonly StringFormat node_number_format = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        public Point Location { get; }
        public int Number { get; }
        public List<Edge> Edges { get; }

        /// <summary>
        /// Declaration of a node
        /// </summary>
        /// <param name="location">The location in the board</param>
        /// <param name="number">The number associated with the node</param>
        public Node(Point location, int number)
        {
            Location = location;
            Number = number;
            Edges = new List<Edge>();
        }

        /// <summary>
        /// Adding an edge to the node
        /// </summary>
        /// <param name="target">To which node</param>
        /// <param name="weight">Specifies the weight of the edge</param>
        public void AddEdge(Node target, int weight)
        {
            Edges.Add(new Edge(target, weight));
        }

        /// <summary>
        /// Defines which coordinates have a point
        /// </summary>
        /// <param name="point">The coordinates of the point</param>
        /// <returns>true if there is already a point</returns>
        public bool ContainsPoint(Point point)
        {
            int dx = point.X - Location.X;
            int dy = point.Y - Location.Y;
            return (dx * dx) + (dy * dy) <= Radius * Radius;
        }

        /// <summary>
        /// Defines the drawing on the board
        /// </summary>
        /// <param name="graphics">The graphics class of the WinForms application</param>
        public void Draw(Graphics graphics)
        {
            int x = Location.X - Radius;
            int y = Location.Y - Radius;
            int diameter = Radius * 2;

            foreach (Edge edge in Edges)
            {
                Point target_location = edge.Target.Location;
                // Draw the line
                graphics.DrawLine(Pens.Black, Location, target_location);

                // Draw the edge weight
                Point mid_point = new Point((Location.X + target_location.X) / 2, (Location.Y + target_location.Y) / 2);
                graphics.DrawString(edge.Weight.ToString(), node_number_font, node_number_brush, mid_point, node_number_format);
            }
            graphics.DrawEllipse(Pens.Black, x, y, diameter, diameter);
            graphics.FillEllipse(Brushes.Aqua, x, y, diameter, diameter);
            graphics.DrawString(Number.ToString(), node_number_font, node_number_brush, Location, node_number_format);
        }


    }
}
