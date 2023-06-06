using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zapoctak
{
    public class DFSAlgorithm
    {
        private Form1 form;
        private int[,] distances;
        private bool[,] visited;
        public bool finished = false;
        int numRows;
        int numCols;
        Stack<Point> stack = new Stack<Point>();

        public DFSAlgorithm(Form1 form)
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

            int startRow = 0;
            int startCol = 0;
            distances[startRow, startCol] = 0;
            visited[startRow, startCol] = true;
            stack.Push(new Point(startCol, startRow));
        }

        public void RunDFS()
        {
            int[] dx = { -1, 0, 1, 0 };
            int[] dy = { 0, 1, 0, -1 };

            if (stack.Count > 0)
            {
                Point currentCell = stack.Pop();

                for (int i = 0; i < dx.Length; i++)
                {
                    int newRow = currentCell.Y + dy[i];
                    int newCol = currentCell.X + dx[i];

                    if (newRow >= 0 && newRow < numRows && newCol >= 0 && newCol < numCols)
                    {
                        if (!visited[newRow, newCol] && form.grid[newRow, newCol] != Form1.square_states.Obstacle)
                        {
                            distances[newRow, newCol] = distances[currentCell.Y, currentCell.X] + 1;
                            visited[newRow, newCol] = true;
                            stack.Push(new Point(newCol, newRow));
                            form.grid[newRow, newCol] = Form1.square_states.Path_helper;
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
            int numRows = form.grid.GetLength(0);
            int numCols = form.grid.GetLength(1);

            if (!visited[numRows - 1, numCols - 1])
            {
                MessageBox.Show("Didn't find a path to end", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
