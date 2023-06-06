using System.Windows.Forms;

namespace Zapoctak
{
    public partial class Form1 : Form
    {
        public enum square_states
        {
            Empty,
            Obstacle,
            Path_helper,
            Path
        }
        private enum algorithm_picked
        {
            BFS,
            DFS
        }
        public const int squareSize = 25;
        public square_states[,] grid = new square_states[20, 20];
        private BFSAlgorithm bfsAlgorithm;
        private DFSAlgorithm dfsAlgorithm;
        private RandomMaze randomMaze;
        private int interval;
        private algorithm_picked algorithm_picker;
        public Form1()
        {
            InitializeComponent();
            interval = 500; // Set the interval to 500 milliseconds (0.5 seconds)
            Timer_algorithm_tick.Interval = interval;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Board_MouseDown(object sender, MouseEventArgs e)
        {
            int row = e.Y / squareSize;
            int col = e.X / squareSize;

            grid[row, col] = square_states.Obstacle;

            Board.Invalidate();
        }

        private void Board_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int row = e.Y / squareSize;
                int col = e.X / squareSize;

                try
                {
                    grid[row, col] = square_states.Obstacle;
                    Board.Invalidate();
                }
                catch (Exception excp)
                {
                    Console.Write(excp);
                }
            }

            if (e.Button == MouseButtons.Right)
            {
                int row = e.Y / squareSize;
                int col = e.X / squareSize;

                try
                {
                    if (grid[row, col] != square_states.Path)
                    {
                        grid[row, col] = square_states.Empty;
                        Board.Invalidate();
                    }
                }
                catch (Exception excp)
                {
                    Console.Write(excp);
                }
            }
        }

        private void Board_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    int squareX = col * squareSize;
                    int squareY = row * squareSize;
                    if (row == 0 && col == 0)
                    {
                        g.FillRectangle(Brushes.Green, squareX, squareY, squareSize, squareSize);
                    }
                    else if (row == grid.GetLength(0) - 1 && col == grid.GetLength(1) - 1)
                    {
                        g.FillRectangle(Brushes.Blue, squareX, squareY, squareSize, squareSize);
                    }
                    else if (grid[row, col] == square_states.Obstacle)
                    {
                        g.FillRectangle(Brushes.Black, col * squareSize, row * squareSize, squareSize, squareSize);
                    }
                    else if (grid[row, col] == square_states.Path_helper)
                    {
                        g.FillRectangle(Brushes.Turquoise, col * squareSize, row * squareSize, squareSize, squareSize);
                    }
                    else if (grid[row, col] == square_states.Path)
                    {
                        g.FillRectangle(Brushes.Yellow, col * squareSize, row * squareSize, squareSize, squareSize);
                    }
                    else
                    {
                        g.FillRectangle(Brushes.White, col * squareSize, row * squareSize, squareSize, squareSize);
                    }
                    g.DrawRectangle(Pens.Black, squareX, squareY, squareSize, squareSize);
                }
            }
        }

        private void reset_board()
        {
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    grid[row, col] = square_states.Empty;
                }
            }
            Board.Invalidate();
        }
        private void Button_reset_Click(object sender, EventArgs e)
        {
            reset_board();
            Timer_algorithm_tick.Stop();
        }



        private void Button_BFS_Click(object sender, EventArgs e)
        {
            bfsAlgorithm = new BFSAlgorithm(this);
            algorithm_picker = algorithm_picked.BFS;
            Timer_algorithm_tick.Start();
        }

        private void Timer_Algorithm_Tick_Tick(object sender, EventArgs e)
        {
            if (algorithm_picker == algorithm_picked.BFS)
            {
                bfsAlgorithm.RunBFS();
                Board.Invalidate();
                if (bfsAlgorithm.finished)
                {
                    Timer_algorithm_tick.Stop();
                    bfsAlgorithm.TraceShortestPath();
                    //MessageBox.Show("BFS algorithm finished!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (algorithm_picker == algorithm_picked.DFS)
            {
                dfsAlgorithm.RunDFS();
                Board.Invalidate();
                if (dfsAlgorithm.finished)
                {
                    Timer_algorithm_tick.Stop();
                    dfsAlgorithm.TraceShortestPath();
                    //MessageBox.Show("DFS algorithm finished!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ScrollBar_Algorithm_Speed_ValueChanged(object sender, EventArgs e)
        {
            Timer_algorithm_tick.Interval = (ScrollBar_Algorithm_Speed.Value + 1);
        }

        private void Button_DFS_Click(object sender, EventArgs e)
        {
            dfsAlgorithm = new DFSAlgorithm(this);
            algorithm_picker = algorithm_picked.DFS;
            Timer_algorithm_tick.Start();
        }

        private void Button_Random_maze_generator_Click(object sender, EventArgs e)
        {
            randomMaze = new RandomMaze(this);
            randomMaze.GenerateRandomMaze();
            Board.Invalidate();
        }
    }
}