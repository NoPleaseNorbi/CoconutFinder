using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{
    public class Edge
    {
        public Node Target { get; }
        public int Weight { get; }

        public Edge(Node target, int weight)
        {
            Target = target;
            Weight = weight;
        }
    }
}
