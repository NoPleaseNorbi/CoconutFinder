using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PathFinder
{
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

        public bool Finished() {
            return finished;
        }
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
    }
}
