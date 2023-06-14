using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PathFinder
{
    /// <summary>
    /// The main class for the DFS algorithm.
    /// </summary>
    public class DFSAlgorithm
    {
        private Form1 form;
        private int[,] distances;
        private bool[,] visited;
        private bool finished;
        int rows_number;
        int cols_number;
        Stack<Point> stack;
        private Point starting_point;
        private Point ending_point;

        /// <summary>
        /// The constructor of the DFS algorithm class
        /// </summary>
        /// <param name="form">The main form of our application</param>
        public DFSAlgorithm(Form1 form)
        {
            this.form = form;
            rows_number = form.grid.GetLength(0);
            cols_number = form.grid.GetLength(1);
            distances = new int[rows_number, cols_number];
            visited = new bool[rows_number, cols_number];
            stack = new Stack<Point>();
            starting_point = form.start_point;
            ending_point = form.end_point;
            for (int row = 0; row < rows_number; row++)
            {
                for (int col = 0; col < cols_number; col++)
                {
                    distances[row, col] = int.MaxValue;
                    visited[row, col] = false;
                }
            }

            distances[starting_point.Y, starting_point.X] = 0;
            visited[starting_point.Y, starting_point.X] = true;
            stack.Push(new Point(starting_point.Y, starting_point.X));
        }

        /// <summary>
        /// Function for determining if the algorithm has already finished
        /// </summary>
        /// <returns>Returns true if the algorithm has finished</returns>
        public bool Finished() 
        {
            return finished;
        }

        /// <summary>
        /// The main function for the DFS algorithm, it uses if and not while,
        /// because we don't want the application to run the whole algorithm in one tick
        /// of our timer.
        /// </summary>
        public void RunDFS()
        {
            int[] dx = { -1, 0, 1, 0 };
            int[] dy = { 0, 1, 0, -1 };

            if (stack.Count > 0)
            {
                Point current_cell = stack.Pop();

                for (int i = 0; i < dx.Length; i++)
                {
                    int new_row = current_cell.Y + dy[i];
                    int new_col = current_cell.X + dx[i];

                    if (new_row >= 0 && new_row < rows_number && new_col >= 0 && new_col < cols_number)
                    {
                        if (!visited[new_row, new_col] && form.grid[new_row, new_col] != Form1.square_states.Obstacle)
                        {
                            distances[new_row, new_col] = distances[current_cell.Y, current_cell.X] + 1;
                            visited[new_row, new_col] = true;
                            stack.Push(new Point(new_col, new_row));
                            form.grid[new_row, new_col] = Form1.square_states.Path_helper;
                        }
                    }
                }
            }
            else
            {
                finished = true;
            }
        }


        /// <summary>
        /// The reconstruction of our path from start to the finish
        /// </summary>
        public void TraceShortestPath()
        {
            if (!visited[ending_point.Y, ending_point.X])
            {
                form.Label_Information.Text = "DFS didn't find a path to end";
                return;
            }

            int current_row = ending_point.Y;
            int current_col = ending_point.X;
            while (current_row != 0 || current_col != 0)
            {
                form.grid[current_row, current_col] = Form1.square_states.Path;

                int[] dx = { -1, 0, 1, 0 };
                int[] dy = { 0, 1, 0, -1 };
                int minimal_distance = int.MaxValue;
                int next_row = -1;
                int next_col = -1;
                for (int i = 0; i < dx.Length; i++)
                {
                    int new_row = current_row + dy[i];
                    int new_col = current_col + dx[i];

                    if (new_row >= 0 && new_row < rows_number && new_col >= 0 && new_col < cols_number)
                    {
                        if (distances[new_row, new_col] < minimal_distance)
                        {
                            minimal_distance = distances[new_row, new_col];
                            next_row = new_row;
                            next_col = new_col;
                        }
                    }
                }

                current_row = next_row;
                current_col = next_col;
            }
            form.Board.Invalidate();
        }
        public List<Node> DFS_for_weighted(Node startNode, Node endNode)
        {
            HashSet<Node> visitedNodes = new HashSet<Node>();
            Stack<Node> stack = new Stack<Node>();
            Dictionary<Node, Node> previousNodes = new Dictionary<Node, Node>();

            stack.Push(startNode);
            visitedNodes.Add(startNode);

            while (stack.Count > 0)
            {
                Node currentNode = stack.Pop();

                if (currentNode == endNode)
                {
                    break;
                }

                foreach (Edge edge in currentNode.Edges)
                {
                    Node neighborNode = edge.Target;

                    if (!visitedNodes.Contains(neighborNode))
                    {
                        stack.Push(neighborNode);
                        visitedNodes.Add(neighborNode);
                        previousNodes[neighborNode] = currentNode;
                    }
                }
            }

            List<Node> shortestPath = new List<Node>();
            Node backtrackNode = endNode;

            while (backtrackNode != null)
            {
                shortestPath.Insert(0, backtrackNode);
                backtrackNode = previousNodes.ContainsKey(backtrackNode) ? previousNodes[backtrackNode] : null;
            }

            if (shortestPath.Count == 1)
            {
                MessageBox.Show("Didn't find a path", "Invalid Nodes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return shortestPath;
        }


    }
}
