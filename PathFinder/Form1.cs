using Microsoft.VisualBasic;
using System.Windows.Forms;
using Zapoctak;

namespace PathFinder
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
            DFS,
            Dijkstra,
            AStar
        }
        public const int squareSize = 25;
        public square_states[,] grid = new square_states[20, 20];
        private BFSAlgorithm bfsAlgorithm;
        private DFSAlgorithm dfsAlgorithm;
        private DijkstraAlgorithm dijkstraAlgorithm;
        private AStarAlgorithm aStarAlgorithm;
        private RandomMaze randomMaze;
        private algorithm_picked algorithm_picker;
        private int interval;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            interval = ScrollBar_Algorithm_Speed.Maximum;
            Timer_algorithm_tick.Interval = interval - ScrollBar_Algorithm_Speed.Value + 1;
            Label_Information.Text = "Welcome";
        }

        private void Board_MouseDown(object sender, MouseEventArgs e)
        {
            int row = e.Y / squareSize;
            int col = e.X / squareSize;
            if (e.Button == MouseButtons.Left)
            {
                grid[row, col] = square_states.Obstacle;
            }
            else if (e.Button == MouseButtons.Right)
            {
                grid[row, col] = square_states.Empty;
            }

            Board.Invalidate();
        }

        private void Board_MouseMove(object sender, MouseEventArgs e)
        {
            int row = e.Y / squareSize;
            int col = e.X / squareSize;
            if (e.Button == MouseButtons.Left)
            {
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
                Label_Information.Text = "BFS algorithm running";
                Board.Invalidate();
                if (bfsAlgorithm.finished)
                {
                    Timer_algorithm_tick.Stop();
                    Label_Information.Text = "BFS algorithm finished";
                    bfsAlgorithm.TraceShortestPath();
                    //MessageBox.Show("BFS algorithm finished!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (algorithm_picker == algorithm_picked.DFS)
            {
                dfsAlgorithm.RunDFS();
                Label_Information.Text = "DFS algorithm running";
                Board.Invalidate();
                if (dfsAlgorithm.finished)
                {
                    Timer_algorithm_tick.Stop();
                    Label_Information.Text = "DFS algorithm finished";
                    dfsAlgorithm.TraceShortestPath();
                    //MessageBox.Show("DFS algorithm finished!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            else if (algorithm_picker == algorithm_picked.Dijkstra)
            {
                dijkstraAlgorithm.RunDijkstra();
                Label_Information.Text = "Dijkstra algorithm running";
                Board.Invalidate();
                if (dijkstraAlgorithm.finished)
                {
                    Timer_algorithm_tick.Stop();
                    Label_Information.Text = "Dijkstra algorithm finished";
                    dijkstraAlgorithm.TraceShortestPath();
                    //MessageBox.Show("Dijkstra algorithm finished!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            else if (algorithm_picker == algorithm_picked.AStar)
            {
                aStarAlgorithm.RunAStar();
                Label_Information.Text = "A Star algorithm running";
                Board.Invalidate();
                if (aStarAlgorithm.finished)
                {

                    Timer_algorithm_tick.Stop();
                    Label_Information.Text = "A Star algorithm finished";
                    aStarAlgorithm.ReconstructPath();
                    //MessageBox.Show("Bidirectional BFS algorithm finished!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ScrollBar_Algorithm_Speed_ValueChanged(object sender, EventArgs e)
        {
            Timer_algorithm_tick.Interval = interval - (ScrollBar_Algorithm_Speed.Value - 1);
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Button_Dijkstra_Click(object sender, EventArgs e)
        {
            dijkstraAlgorithm = new DijkstraAlgorithm(this);
            algorithm_picker = algorithm_picked.Dijkstra;
            Timer_algorithm_tick.Start();
        }

        private void Button_AStar_Click(object sender, EventArgs e)
        {
            aStarAlgorithm = new AStarAlgorithm(this);
            algorithm_picker = algorithm_picked.AStar;
            Timer_algorithm_tick.Start();
        }
    }
}