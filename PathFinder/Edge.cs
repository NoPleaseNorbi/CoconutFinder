using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{
    /// <summary>
    /// The class for our edges
    /// </summary>
    public class Edge
    {
        public Node Target { get; }
        public int Weight { get; }

        /// <summary>
        /// The constructor of our edge class
        /// </summary>
        /// <param name="target">The node where the edge is heading</param>
        /// <param name="weight">The weight of the edge</param>
        public Edge(Node target, int weight)
        {
            Target = target;
            Weight = weight;
        }
    }
}
