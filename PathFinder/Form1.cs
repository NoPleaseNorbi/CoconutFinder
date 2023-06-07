using Microsoft.VisualBasic;
using System.Windows.Forms;

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
        public Point start_point;
        public Point end_point;
        public square_states[,] grid;
        
        private int square_size;
        private int grid_size;
        private BFSAlgorithm BFS_Algorithm;
        private DFSAlgorithm DFS_Algorithm;
        private DijkstraAlgorithm Dijkstra_Algorithm;
        private AStarAlgorithm AStar_Algorithm;
        private RandomMaze random_maze;
        private algorithm_picked algorithm_picker;
        private int interval;
        public Form1()
        {
            InitializeComponent();
            square_size = 25;
            grid_size = Board.Width / square_size;
            grid = new square_states[grid_size, grid_size];
            start_point = new Point(0, 0);
            end_point = new Point(grid_size - 1, grid_size - 1);
            BFS_Algorithm = new BFSAlgorithm(this);
            DFS_Algorithm = new DFSAlgorithm(this);
            Dijkstra_Algorithm = new DijkstraAlgorithm(this);
            AStar_Algorithm = new AStarAlgorithm(this);
            random_maze = new RandomMaze(this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            interval = ScrollBar_Algorithm_Speed.Maximum;
            Timer_algorithm_tick.Interval = interval - ScrollBar_Algorithm_Speed.Value + 1;
            Label_Information.Text = "Welcome";
        }

        private void Board_MouseDown(object sender, MouseEventArgs e)
        {
            int row = e.Y / square_size;
            int col = e.X / square_size;
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
            int row = e.Y / square_size;
            int col = e.X / square_size;
            if (e.Button == MouseButtons.Left)
            {
                try
                {
                    grid[row, col] = square_states.Obstacle;
                    Board.Invalidate();
                }
                catch (Exception excp)
                {
                    MessageBox.Show(excp.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show(excp.Message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    int square_x = col * square_size;
                    int square_y = row * square_size;
                    if (row == start_point.X && col == start_point.Y)
                    {
                        g.FillRectangle(Brushes.Green, square_x, square_y, square_size, square_size);
                    }
                    else if (row == end_point.X && col == end_point.Y)
                    {
                        g.FillRectangle(Brushes.Blue, square_x, square_y, square_size, square_size);
                    }
                    else if (grid[row, col] == square_states.Obstacle)
                    {
                        g.FillRectangle(Brushes.Black, col * square_size, row * square_size, square_size, square_size);
                    }
                    else if (grid[row, col] == square_states.Path_helper)
                    {
                        g.FillRectangle(Brushes.Turquoise, col * square_size, row * square_size, square_size, square_size);
                    }
                    else if (grid[row, col] == square_states.Path)
                    {
                        g.FillRectangle(Brushes.Yellow, col * square_size, row * square_size, square_size, square_size);
                    }
                    else
                    {
                        g.FillRectangle(Brushes.White, col * square_size, row * square_size, square_size, square_size);
                    }
                    g.DrawRectangle(Pens.Black, square_x, square_y, square_size, square_size);
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
            BFS_Algorithm = new BFSAlgorithm(this);
            algorithm_picker = algorithm_picked.BFS;
            Timer_algorithm_tick.Start();
        }

        private void Timer_Algorithm_Tick_Tick(object sender, EventArgs e)
        {
            if (algorithm_picker == algorithm_picked.BFS)
            {
                BFS_Algorithm.RunBFS();
                Label_Information.Text = "BFS algorithm running";
                Board.Invalidate();
                if (BFS_Algorithm.Finished())
                {
                    Timer_algorithm_tick.Stop();
                    Label_Information.Text = "BFS algorithm finished";
                    BFS_Algorithm.TraceShortestPath();
                }
            }
            else if (algorithm_picker == algorithm_picked.DFS)
            {
                DFS_Algorithm.RunDFS();
                Label_Information.Text = "DFS algorithm running";
                Board.Invalidate();
                if (DFS_Algorithm.Finished())
                {
                    Timer_algorithm_tick.Stop();
                    Label_Information.Text = "DFS algorithm finished";
                    DFS_Algorithm.TraceShortestPath();
                }
            }

            else if (algorithm_picker == algorithm_picked.Dijkstra)
            {
                Dijkstra_Algorithm.RunDijkstra();
                Label_Information.Text = "Dijkstra algorithm running";
                Board.Invalidate();
                if (Dijkstra_Algorithm.Finished())
                {
                    Timer_algorithm_tick.Stop();
                    Label_Information.Text = "Dijkstra algorithm finished";
                    Dijkstra_Algorithm.TraceShortestPath();
                }
            }

            else if (algorithm_picker == algorithm_picked.AStar)
            {
                if (AStar_Algorithm.Finished())
                {
                    Timer_algorithm_tick.Stop();
                    Label_Information.Text = "A Star algorithm finished";
                    AStar_Algorithm.ReconstructPath();
                }
                AStar_Algorithm.RunAStar();
                if (!AStar_Algorithm.Finished())
                    Label_Information.Text = "A Star algorithm running";
                Board.Invalidate();
            }
        }

        private void ScrollBar_Algorithm_Speed_ValueChanged(object sender, EventArgs e)
        {
            Timer_algorithm_tick.Interval = interval - (ScrollBar_Algorithm_Speed.Value - 1);
        }

        private void Button_DFS_Click(object sender, EventArgs e)
        {
            DFS_Algorithm = new DFSAlgorithm(this);
            algorithm_picker = algorithm_picked.DFS;
            Timer_algorithm_tick.Start();
        }

        private void Button_Random_maze_generator_Click(object sender, EventArgs e)
        {
            random_maze = new RandomMaze(this);
            random_maze.GenerateRandomMaze();
            Board.Invalidate();
        }

        private void Button_Dijkstra_Click(object sender, EventArgs e)
        {
            Dijkstra_Algorithm = new DijkstraAlgorithm(this);
            algorithm_picker = algorithm_picked.Dijkstra;
            Timer_algorithm_tick.Start();
        }

        private void Button_AStar_Click(object sender, EventArgs e)
        {
            AStar_Algorithm = new AStarAlgorithm(this);
            algorithm_picker = algorithm_picked.AStar;
            Timer_algorithm_tick.Start();
        }
    }
}