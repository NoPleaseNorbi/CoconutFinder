using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PathFinder
{
    public class DijkstraAlgorithm
    {
        private Form1 form;
        private int numRows;
        private int numCols;
        private int[,] distances;
        private bool[,] visited;
        public bool finished = false;

        public DijkstraAlgorithm(Form1 form)
        {
            this.form = form;
            numRows = form.grid.GetLength(0);
            numCols = form.grid.GetLength(1);
            distances = new int[numRows, numCols];
            visited = new bool[numRows, numCols];

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    distances[row, col] = int.MaxValue;
                    visited[row, col] = false;
                }
            }
            distances[0, 0] = 0;
        }

        public void RunDijkstra() {
            int minDistance = int.MaxValue;
            Point closestNode = new Point(-1, -1);

            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    if (!visited[row, col] && distances[row, col] < minDistance)
                    {
                        minDistance = distances[row, col];
                        closestNode.X = col;
                        closestNode.Y = row;
                    }
                }
            }
            if (closestNode.X == -1 || closestNode.Y == -1)
            {
                finished = true;
                return;
            }
            visited[closestNode.Y, closestNode.X] = true;

            Point currentCell = closestNode;
            
            int[] dx = { -1, 0, 1, 0 };
            int[] dy = { 0, 1, 0, -1 };

            for (int i = 0; i < dx.Length; i++)
            {
                int newRow = currentCell.Y + dy[i];
                int newCol = currentCell.X + dx[i];

                if (newRow >= 0 && newRow < numRows && newCol >= 0 && newCol < numCols)
                {
                    int distance = distances[currentCell.Y, currentCell.X] + 1;
                    if (distance < distances[newRow, newCol] && form.grid[newRow, newCol] != Form1.square_states.Obstacle)
                    {
                        distances[newRow, newCol] = distance;
                        form.grid[newRow, newCol] = Form1.square_states.Path_helper;
                    }
                }
            }
        }

        public void TraceShortestPath()
        {
            if (distances[numRows - 1, numCols - 1] == int.MaxValue)
            {
                form.Label_Information.Text = "Didn't find a path to end";
                //MessageBox.Show("Didn't find a path to the end", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int currentRow = numRows - 1;
            int currentCol = numCols - 1;

            while (currentRow != 0 || currentCol != 0)
            {
                form.grid[currentRow, currentCol] = Form1.square_states.Path;

                int[] dx = { -1, 0, 1, 0 };
                int[] dy = { 0, 1, 0, -1 };
                int minDistance = int.MaxValue;
                int nextRow = -1;
                int nextCol = -1;

                for (int i = 0; i < dx.Length; i++)
                {
                    int newRow = currentRow + dy[i];
                    int newCol = currentCol + dx[i];

                    if (newRow >= 0 && newRow < numRows && newCol >= 0 && newCol < numCols)
                    {
                        if (distances[newRow, newCol] < minDistance)
                        {
                            minDistance = distances[newRow, newCol];
                            nextRow = newRow;
                            nextCol = newCol;
                        }
                    }
                }

                currentRow = nextRow;
                currentCol = nextCol;
            }

            form.Board.Invalidate();
        }
    }
}
