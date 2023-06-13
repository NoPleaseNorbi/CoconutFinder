using Microsoft.VisualBasic;
using System.Windows.Forms;

namespace PathFinder
{
    /// <summary>
    /// The main form for the PathFinder application.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// Represents the possible states of a square on the grid.
        /// </summary>
        public enum square_states
        {
            Empty,       ///< The square is empty.
            Obstacle,    ///< The square is an obstacle.
            Path_helper, ///< The square is being searched trough using one of the algorithms.
            Path         ///< The square is the path from the starting node to the finish node.
        }
        /// <summary>
        /// Represents the available algorithms for pathfinding.
        /// </summary>
        private enum algorithm_picked
        {
            BFS,      ///< Breadth First Search
            DFS,      ///< Depth First Search
            Dijkstra, ///< Dijkstra's algorithm
            AStar     ///< A star algorithm
        }
        /// <summary>
        /// The starting point for pathfinding.
        /// </summary>
        public Point start_point;

        /// <summary>
        /// The ending point for pathfinding.
        /// </summary>
        public Point end_point;

        /// <summary>
        /// The grid representing the state of each square.
        /// </summary>
        public square_states[,] grid;

        /// <summary>
        /// The size of our square
        /// </summary>
        private int square_size;

        /// <summary>
        /// The size of the grid, in this case the width of <see cref="Board"/>
        /// </summary>
        private int grid_size;

        /// <summary>
        /// The class for our BFS algorithm
        /// </summary>
        private BFSAlgorithm BFS_Algorithm;

        /// <summary>
        /// The class for our DFS algorithm
        /// </summary>
        private DFSAlgorithm DFS_Algorithm;

        /// <summary>
        /// The class for our Dijkstra's algorithm
        /// </summary>
        private DijkstraAlgorithm Dijkstra_Algorithm;

        /// <summary>
        /// The class for our A star algorithm
        /// </summary>
        private AStarAlgorithm AStar_Algorithm;

        /// <summary>
        /// The class for our random maze generator algorithm
        /// </summary>
        private RandomMaze random_maze;

        /// <summary>
        /// Indicates which algorithm is currently picked
        /// </summary>
        private algorithm_picked algorithm_picker;
        private int interval;

        /// <summary>
        /// The constructor of our form
        /// </summary>
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

        /// <summary>
        /// The loading of the form
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            interval = ScrollBar_Algorithm_Speed.Maximum;
            Timer_algorithm_tick.Interval = interval - ScrollBar_Algorithm_Speed.Value + 1;
            Label_Information.Text = "Welcome";
        }

        /// <summary>
        /// Dealing with mouse being pressed. If the user presses left mouse button,
        /// the program adds and obstacle, if the right mouse button is used, 
        /// then we delete an obstacle
        /// </summary>
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

        /// <summary>
        /// The same principle applies here aswell as in the mouse down function, but here we trace 
        /// the movement of the mouse
        /// </summary>
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
                    //If we hold the mouse down and we move it outsite of our board, we show a warning</value>
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

        /// <summary>
        /// In this function we paint our board. The manual explains the colours of the squares.
        /// </summary>
        private void Board_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    int square_x = col * square_size;
                    int square_y = row * square_size;
                    if (row == start_point.Y && col == start_point.X)
                    {
                        g.FillRectangle(Brushes.Green, square_x, square_y, square_size, square_size);
                    }
                    else if (row == end_point.Y && col == end_point.X)
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
                    g.DrawRectangle(Pens.Black, square_x, square_y, square_size, square_size); // Here we paint the edges of our squares
                }
            }
        }

        /// <summary>
        /// This function resets the board, and paints every square white
        /// </summary>
        private void Reset_Board()
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

        /// <summary>
        /// If the reset button is clicked, it calls the <see cref="Reset_Board">
        /// </summary>
        private void Button_reset_Click(object sender, EventArgs e)
        {
            Reset_Board();
            Timer_algorithm_tick.Stop();
        }

        /// <summary>
        /// Calls the BFS algorithm if the "BFS" button is clicked
        /// </summary>
        private void Button_BFS_Click(object sender, EventArgs e)
        {
            BFS_Algorithm = new BFSAlgorithm(this);
            algorithm_picker = algorithm_picked.BFS;
            Timer_algorithm_tick.Start();
        }

        /// <summary>
        /// This class uses the <see cref="algorithm_picked"/> enumerator to determine
        /// which algorithm has been called and according to it updates the <see cref="Label_Information">
        /// Using the <see cref="ScrollBar_Algorithm_Speed"> it animates the algorithms 
        /// </summary>
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

        /// <summary>
        /// If the value of the <see cref="ScrollBar_Algorithm_Speed"/> is changed,
        /// we change the <see cref="Timer_algorithm_tick"/> interval
        /// </summary>
        private void ScrollBar_Algorithm_Speed_ValueChanged(object sender, EventArgs e)
        {
            Timer_algorithm_tick.Interval = interval - (ScrollBar_Algorithm_Speed.Value - 1);
        }

        /// <summary>
        /// Calls the DFS algorithm if the "DFS" button is clicked
        /// </summary>
        private void Button_DFS_Click(object sender, EventArgs e)
        {
            DFS_Algorithm = new DFSAlgorithm(this);
            algorithm_picker = algorithm_picked.DFS;
            Timer_algorithm_tick.Start();
        }

        /// <summary>
        /// This function calls our maze generator class, to generate a random maze.
        /// </summary>
        private void Button_Random_maze_generator_Click(object sender, EventArgs e)
        {
            Timer_algorithm_tick.Stop();
            random_maze = new RandomMaze(this);
            random_maze.GenerateRandomMaze();
            Board.Invalidate();
        }

        /// <summary>
        /// Calls the Dijsktra's algorithm if the "Dijkstra" button is clicked
        /// </summary>
        private void Button_Dijkstra_Click(object sender, EventArgs e)
        {
            Dijkstra_Algorithm = new DijkstraAlgorithm(this);
            algorithm_picker = algorithm_picked.Dijkstra;
            Timer_algorithm_tick.Start();
        }

        /// <summary>
        /// Calls the AStar algorithm if the "A Star" button is clicked
        /// </summary>
        private void Button_AStar_Click(object sender, EventArgs e)
        {
            AStar_Algorithm = new AStarAlgorithm(this);
            algorithm_picker = algorithm_picked.AStar;
            Timer_algorithm_tick.Start();
        }
    }
}