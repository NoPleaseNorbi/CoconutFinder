using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{
    public class RandomMaze
    {
        private int numRows;
        private int numCols;
        private Random random;
        private Form1 form;
        public RandomMaze(Form1 form)
        {
            this.form = form;
            numRows = form.grid.GetLength(0);
            numCols = form.grid.GetLength(1);
            random = new Random();
        }

        public void GenerateRandomMaze() { 
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    if (i == 0 && j == 0) { continue; }
                    else if (i == numRows - 1 && j == numCols - 1) { continue; }
                    if (random.Next(0, 3) == 0)
                    {
                        form.grid[i, j] = Form1.square_states.Obstacle;
                    }
                    else {
                        form.grid[i, j] = Form1.square_states.Empty;
                    }
                }
            }
        }
    }
}

