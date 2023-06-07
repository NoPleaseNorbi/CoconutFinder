using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{
    public class RandomMaze
    {
        private int rows_number;
        private int cols_number;
        private Random random;
        private Form1 form;
        private Point starting_point;
        private Point ending_point;
        public RandomMaze(Form1 form)
        {
            this.form = form;
            rows_number = form.grid.GetLength(0);
            cols_number = form.grid.GetLength(1);
            random = new Random();
            starting_point = form.start_point;
            ending_point = form.end_point;
        }

        public void GenerateRandomMaze() 
        { 
            for (int i = 0; i < rows_number; i++)
            {
                for (int j = 0; j < cols_number; j++)
                {
                    if (i == starting_point.Y && j == starting_point.X) { continue; }
                    else if (i == ending_point.Y && j == ending_point.X) { continue; }
                    if (random.Next(0, 3) == 0)
                    {
                        form.grid[i, j] = Form1.square_states.Obstacle;
                    }
                    else 
                    {
                        form.grid[i, j] = Form1.square_states.Empty;
                    }
                }
            }
        }
    }
}

