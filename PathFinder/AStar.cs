using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PathFinder
{
    /// <summary>
    /// The main class for the A star algorithm.
    /// </summary>
    public class AStarAlgorithm
    {
        /// <summary>
        /// The main form of our application
        /// </summary>
        private Form1 form;
        private int rows_number;
        private int cols_number;
        private bool[,] visited;
        private int[,] distances;
        private int[,] heuristic;
        private PriorityQueue<Point, int> open_set;
        private Dictionary<Point, Point> came_from;
        private bool finished;
        private Point starting_point;
        private Point ending_point;

        /// <summary>
        /// The constructor of the A star algorithm class
        /// </summary>
        /// <param name="form">The main form of our application</param>
        public AStarAlgorithm(Form1 form)
        {
            this.form = form;
            rows_number = form.grid.GetLength(0);
            cols_number = form.grid.GetLength(1);
            visited = new bool[rows_number, cols_number];
            distances = new int[rows_number, cols_number];
            heuristic = new int[rows_number, cols_number];
            open_set = new PriorityQueue<Point, int>();
            came_from = new Dictionary<Point, Point>();
            starting_point = form.start_point;
            ending_point = form.end_point;
            for (int row = 0; row < rows_number; row++)
            {
                for (int col = 0; col < cols_number; col++)
                {
                    visited[row, col] = false;
                    distances[row, col] = int.MaxValue;
                    heuristic[row, col] = CalculateHeuristic(new Point(col, row));
                }
            }

            distances[starting_point.X, starting_point.Y] = 0;

            open_set.Enqueue(new Point(starting_point.X, starting_point.Y), 0);
        }

        /// <summary>
        /// The main function for the A star algorithm, it uses if and not while,
        /// because we don't want the application to run the whole algorithm in one tick
        /// of our timer.
        /// </summary>
        public void RunAStar()
        {
            if (open_set.Count > 0)
            {
                Point current_cell = open_set.Dequeue();

                if (current_cell == ending_point)
                {
                    finished = true;
                    return;
                }

                visited[current_cell.Y, current_cell.X] = true;

                ExploreNeighbors(current_cell);
            }
            else {
                finished = true;
                form.Label_Information.Text = "A Star didn't find a path to end";
                form.Timer_algorithm_tick.Stop();
                return;
            }
        }

        /// <summary>
        /// We explore the neighbours of our current_cell, meaning we explore the squares,
        /// which are next to our current_cell
        /// </summary>
        /// <param name="current_cell">The current cell, for what we need the neighbours</param>
        private void ExploreNeighbors(Point current_cell)
        {
            int[] dx = { -1, 0, 1, 0 };
            int[] dy = { 0, 1, 0, -1 };

            for (int i = 0; i < dx.Length; i++)
            {
                int new_row = current_cell.Y + dy[i];
                int new_col = current_cell.X + dx[i];

                if (new_row >= 0 && new_row < rows_number && new_col >= 0 && new_col < cols_number)
                {
                    if (!visited[new_row, new_col] && form.grid[new_row, new_col] != Form1.square_states.Obstacle)
                    {
                        int tent_distance = distances[current_cell.Y, current_cell.X] + 1;

                        if (tent_distance < distances[new_row, new_col])
                        {
                            distances[new_row, new_col] = tent_distance;

                            int priority = tent_distance + heuristic[new_row, new_col];
                            open_set.Enqueue(new Point(new_col, new_row), priority);

                            came_from[new Point(new_col, new_row)] = current_cell;
                            form.grid[new_row, new_col] = Form1.square_states.Path_helper;
                        }
                    }
                }
            }
            form.Board.Invalidate();
        }

        /// <summary>
        /// Reconstruction of the path.
        /// </summary>
        public void ReconstructPath()
        {
            int curr_row = rows_number - 1;
            int curr_col = cols_number - 1;

            while (curr_row != 0 || curr_col != 0)
            {
                form.grid[curr_row, curr_col] = Form1.square_states.Path;

                Point current_cell = new Point(curr_col, curr_row);
                current_cell = came_from[current_cell];

                curr_row = current_cell.Y;
                curr_col = current_cell.X;
            }
            form.Board.Invalidate();
        }

        /// <summary>
        /// Calculates the estimated distance of our current cell
        /// </summary>
        /// <param name="cell">The cell for what we need the distance</param>
        /// <returns>The distance</returns>
        private int CalculateHeuristic(Point cell)
        {
            int dx = Math.Abs(cell.X - (cols_number - 1));
            int dy = Math.Abs(cell.Y - (rows_number - 1));
            return dx + dy;
        }

        /// <summary>
        /// Function for determining if the algorithm hsa already finished
        /// </summary>
        /// <returns>Returns true if the algorithm finished</returns>
        public bool Finished() {
            return finished;
        }

        /// <summary>
        /// Constructs the Astar algorithm for the weighted graph
        /// </summary>
        /// <param name="starting_node">The starting node</param>
        /// <param name="ending_node">The ending node</param>
        /// <param name="nodes">All the nodes in the graph</param>
        /// <returns>The list of nodes present in the path</returns>
        public List<Edge> AStar_for_weighted(Node starting_node, Node ending_node, List<Node> nodes)
        {
            Dictionary<Node, int> distances = new Dictionary<Node, int>();
            Dictionary<Node, Node> prev_nodes = new Dictionary<Node, Node>();
            HashSet<Node> visited = new HashSet<Node>();
            HashSet<Node> unvisited = new HashSet<Node>(nodes);

            foreach (Node node in unvisited)
            {
                distances[node] = int.MaxValue;
            }

            distances[starting_node] = 0;

            while (unvisited.Count > 0)
            {
                Node curr_node = GetClosestNode_for_weighted(distances, unvisited);

                if (curr_node == ending_node)
                {
                    break;
                }

                unvisited.Remove(curr_node);
                visited.Add(curr_node);

                if (curr_node == null) 
                {
                    break;
                }
                if (curr_node.Edges.Count == 0) 
                {
                    break;
                }
                foreach (Edge edge in curr_node.Edges)
                {
                    Node neighbour = edge.Target;

                    if (visited.Contains(neighbour))
                    {
                        continue;
                    }

                    int distance = distances[curr_node] + edge.Weight;

                    if (distance < distances[neighbour])
                    {
                        distances[neighbour] = distance;
                        prev_nodes[neighbour] = curr_node;
                    }
                }
            }

            List<Edge> path = new List<Edge>();
            Node backtrack = ending_node;

            while (backtrack != null)
            {
                Node prev_node;
                if (prev_nodes.TryGetValue(backtrack, out prev_node))
                {
                    Edge edge = GetEdgeBetweenNodes_for_weighted(prev_node, backtrack);
                    if (edge != null)
                    {
                        path.Insert(0, edge);
                    }
                }

                backtrack = prev_node;
            }

            if (path.Count == 0)
            {
                MessageBox.Show("Didn't find a path", "Invalid Nodes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return path;
        }

        private Edge GetEdgeBetweenNodes_for_weighted(Node source, Node target)
        {
            foreach (Edge edge in source.Edges)
            {
                if (edge.Target == target)
                {
                    return edge;
                }
            }

            return null;
        }

        private Node GetClosestNode_for_weighted(Dictionary<Node, int> distances, HashSet<Node> unvisitedNodes)
        {
            int min_dist = int.MaxValue;
            Node closest_node = null;

            foreach (Node node in unvisitedNodes)
            {
                if (distances[node] < min_dist)
                {
                    min_dist = distances[node];
                    closest_node = node;
                }
            }

            return closest_node;
        }




    }
}
