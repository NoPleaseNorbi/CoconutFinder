using PathFinder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Zapoctak
{
    public class AStarAlgorithm
    {
        private Form1 form;
        private int numRows;
        private int numCols;
        private bool[,] visited;
        private int[,] distances;
        private int[,] heuristic;
        private PriorityQueue<Point, int> openSet;
        private Dictionary<Point, Point> cameFrom;
        public bool finished;

        public AStarAlgorithm(Form1 form)
        {
            this.form = form;
            numRows = form.grid.GetLength(0);
            numCols = form.grid.GetLength(1);
            visited = new bool[numRows, numCols];
            distances = new int[numRows, numCols];
            heuristic = new int[numRows, numCols];
            openSet = new PriorityQueue<Point, int>();
            cameFrom = new Dictionary<Point, Point>();
            
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    visited[row, col] = false;
                    distances[row, col] = int.MaxValue;
                    heuristic[row, col] = CalculateHeuristic(new Point(col, row));
                }
            }

            int startRow = 0;
            int startCol = 0;
            distances[startRow, startCol] = 0;

            openSet.Enqueue(new Point(startCol, startRow), 0);
        }

        public void RunAStar()
        {

            if (openSet.Count > 0)
            {
                Point currentCell = openSet.Dequeue();

                if (currentCell == new Point(numCols - 1, numRows - 1))
                {
                    finished = true;
                    return;
                }

                visited[currentCell.Y, currentCell.X] = true;

                ExploreNeighbors(currentCell);
            }
            else {
                form.Timer_algorithm_tick.Stop();
                //finished = true;
                form.Label_Information.Text = "Didn't find a path to end";
                //MessageBox.Show("Didn't find a path to the end", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void ExploreNeighbors(Point currentCell)
        {
            int[] dx = { -1, 0, 1, 0 };
            int[] dy = { 0, 1, 0, -1 };

            for (int i = 0; i < dx.Length; i++)
            {
                int newRow = currentCell.Y + dy[i];
                int newCol = currentCell.X + dx[i];

                if (newRow >= 0 && newRow < numRows && newCol >= 0 && newCol < numCols)
                {
                    if (!visited[newRow, newCol] && form.grid[newRow, newCol] != Form1.square_states.Obstacle)
                    {
                        int tentativeDistance = distances[currentCell.Y, currentCell.X] + 1;

                        if (tentativeDistance < distances[newRow, newCol])
                        {
                            distances[newRow, newCol] = tentativeDistance;

                            int priority = tentativeDistance + heuristic[newRow, newCol];
                            openSet.Enqueue(new Point(newCol, newRow), priority);

                            cameFrom[new Point(newCol, newRow)] = currentCell;
                            form.grid[newRow, newCol] = Form1.square_states.Path_helper;
                        }
                    }
                }
            }

            form.Board.Invalidate();
        }

        public void ReconstructPath()
        {
            int currentRow = numRows - 1;
            int currentCol = numCols - 1;

            while (currentRow != 0 || currentCol != 0)
            {
                form.grid[currentRow, currentCol] = Form1.square_states.Path;

                Point currentCell = new Point(currentCol, currentRow);
                currentCell = cameFrom[currentCell];

                currentRow = currentCell.Y;
                currentCol = currentCell.X;
            }

            form.Board.Invalidate();
        }

        private int CalculateHeuristic(Point cell)
        {
            int dx = Math.Abs(cell.X - (numCols - 1));
            int dy = Math.Abs(cell.Y - (numRows - 1));
            return dx + dy;
        }
    }
}
