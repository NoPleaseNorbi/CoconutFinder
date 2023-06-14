﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PathFinder
{
    /// <summary>
    /// The main class for the Dijkstra algorithm.
    /// </summary>
    public class DijkstraAlgorithm
    {
        private Form1 form;
        private int rows_number;
        private int cols_number;
        private int[,] distances;
        private bool[,] visited;
        private bool finished;
        private Point starting_point;
        private Point ending_point;

        /// <summary>
        /// The constructor of the Dijkstra's algorithm class
        /// </summary>
        /// <param name="form">The main form of our application</param>
        public DijkstraAlgorithm(Form1 form)
        {
            this.form = form;
            rows_number = form.grid.GetLength(0);
            cols_number = form.grid.GetLength(1);
            distances = new int[rows_number, cols_number];
            visited = new bool[rows_number, cols_number];
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
        /// The main function for the Dijkstra's algorithm, it uses if and not while,
        /// because we don't want the application to run the whole algorithm in one tick
        /// of our timer.
        /// </summary>
        public void RunDijkstra() 
        {
            int minimal_distance = int.MaxValue;
            Point closest_node = new Point(-1, -1);

            for (int row = 0; row < rows_number; row++)
            {
                for (int col = 0; col < cols_number; col++)
                {
                    if (!visited[row, col] && distances[row, col] < minimal_distance)
                    {
                        minimal_distance = distances[row, col];
                        closest_node.X = col;
                        closest_node.Y = row;
                    }
                }
            }
            if (closest_node.X == -1 || closest_node.Y == -1)
            {
                finished = true;
                return;
            }
            visited[closest_node.Y, closest_node.X] = true;

            Point current_cell = closest_node;
            
            int[] dx = { -1, 0, 1, 0 };
            int[] dy = { 0, 1, 0, -1 };

            for (int i = 0; i < dx.Length; i++)
            {
                int new_row = current_cell.Y + dy[i];
                int new_col = current_cell.X + dx[i];

                if (new_row >= 0 && new_row < rows_number && new_col >= 0 && new_col < cols_number)
                {
                    int distance = distances[current_cell.Y, current_cell.X] + 1;
                    if (distance < distances[new_row, new_col] && form.grid[new_row, new_col] != Form1.square_states.Obstacle)
                    {
                        distances[new_row, new_col] = distance;
                        form.grid[new_row, new_col] = Form1.square_states.Path_helper;
                    }
                }
            }
        }

        // <summary>
        /// The reconstruction of our path from start to the finish
        /// </summary>
        public void TraceShortestPath()
        {
            if (distances[rows_number - 1, cols_number - 1] == int.MaxValue)
            {
                form.Label_Information.Text = "Dijkstra didn't find a path to end";
                return;
            }

            int current_row = rows_number - 1;
            int current_col = cols_number - 1;

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

        public List<Node> Dijkstra_for_weighted(Node startNode, Node endNode, List<Node> nodes)
        {
            Dictionary<Node, int> distances = new Dictionary<Node, int>();
            Dictionary<Node, Node> previousNodes = new Dictionary<Node, Node>();
            List<Node> unvisitedNodes = new List<Node>(nodes);

            foreach (Node node in unvisitedNodes)
            {
                distances[node] = int.MaxValue;
            }

            distances[startNode] = 0;

            while (unvisitedNodes.Count > 0)
            {
                Node currentNode = unvisitedNodes.OrderBy(node => distances[node]).First();

                if (currentNode == endNode)
                {
                    break;
                }

                unvisitedNodes.Remove(currentNode);

                foreach (Edge edge in currentNode.Edges)
                {
                    int distance = distances[currentNode] + edge.Weight;

                    if (distance < distances[edge.Target])
                    {
                        distances[edge.Target] = distance;
                        previousNodes[edge.Target] = currentNode;
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
