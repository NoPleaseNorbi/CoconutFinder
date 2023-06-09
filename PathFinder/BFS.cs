﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PathFinder
{
    /// <summary>
    /// The main class for the BFS algorithm.
    /// </summary>
    public class BFSAlgorithm
    {
        private Form1 form;
        private int[,] distances;
        private bool[,] visited;
        private bool finished = false;
        int rows_number;
        int cols_number;
        Queue<Point> queue;
        private Point starting_point;
        private Point ending_point;

        /// <summary>
        /// The constructor of the BFS algorithm class
        /// </summary>
        /// <param name="form">The main form of our application</param>
        public BFSAlgorithm(Form1 form)
        {
            this.form = form;
            rows_number = form.grid.GetLength(0);
            cols_number = form.grid.GetLength(1);
            distances = new int[rows_number, cols_number];
            visited = new bool[rows_number, cols_number];
            queue = new Queue<Point>();
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
            queue.Enqueue(new Point(starting_point.Y, starting_point.X));
        }

        /// <summary>
        /// Function for determining if the algorithm has already finished
        /// </summary>
        /// <returns>Returns true if the algorithm has finished</returns>
        public bool Finished() {
            return finished;
        }

        /// <summary>
        /// The main function for the BFS algorithm, it uses if and not while,
        /// because we don't want the application to run the whole algorithm in one tick
        /// of our timer.
        /// </summary>
        public void RunBFS()
        {
            int[] dx = { -1, 0, 1, 0 };
            int[] dy = { 0, 1, 0, -1 };

            if (queue.Count > 0)
            {
                Point current_cell = queue.Dequeue();

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
                            form.grid[new_row, new_col] = Form1.square_states.Path_helper;
                            queue.Enqueue(new Point(new_col, new_row));
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
            if (!visited[ending_point.Y, ending_point.X]) {
                form.Label_Information.Text = "BFS didn't find a path to end";
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
                    int newCol = current_col + dx[i];

                    if (new_row >= 0 && new_row < rows_number && newCol >= 0 && newCol < cols_number)
                    {
                        if (distances[new_row, newCol] < minimal_distance)
                        {
                            minimal_distance = distances[new_row, newCol];
                            next_row = new_row;
                            next_col = newCol;
                        }
                    }
                }

                current_row = next_row;
                current_col = next_col;
            }
            form.Board.Invalidate();
        }

        /// <summary>
        /// Constructs the BFS algorithm for the weighted graph
        /// </summary>
        /// <param name="starting_node">The starting node</param>
        /// <param name="ending_node">The ending node</param>
        /// <returns>The list of nodes present in the path</returns>
        public List<Edge> BFS_for_weighted(Node starting_node, Node ending_node)
        {
            Dictionary<Node, Node> prev_nodes = new Dictionary<Node, Node>();
            Queue<Node> queue = new Queue<Node>();
            HashSet<Node> visited = new HashSet<Node>();

            queue.Enqueue(starting_node);
            visited.Add(starting_node);

            while (queue.Count > 0)
            {
                Node curr_node = queue.Dequeue();

                if (curr_node == ending_node)
                {
                    break;
                }

                foreach (Edge edge in curr_node.Edges)
                {
                    Node neighbor_node = edge.Target;

                    if (!visited.Contains(neighbor_node))
                    {
                        queue.Enqueue(neighbor_node);
                        visited.Add(neighbor_node);
                        prev_nodes[neighbor_node] = curr_node;
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
                    Edge edge = GetEdgeBetweenNodes(prev_node, backtrack);
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

        private Edge GetEdgeBetweenNodes(Node source, Node target)
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


    }
}
