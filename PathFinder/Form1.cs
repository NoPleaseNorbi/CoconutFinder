using Microsoft.VisualBasic;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;

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
        private int current_number;
        private List<Node> nodes = new List<Node>();
        public bool weighted_graph_selected;
        private List<Node> weighted_graph_path = new List<Node>();
        /// <summary>
        /// The constructor of our form
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            weighted_graph_selected = false;
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
            if (weighted_graph_selected)
            {
                if (int.TryParse(textBox_StartingNode.Text, out int startNumber) && int.TryParse(textBox_EndingNode.Text, out int endNumber))
                {
                    Node starting_node = FindNodeByNumber(startNumber);
                    Node ending_node = FindNodeByNumber(endNumber);

                    if (starting_node != null && ending_node != null)
                    {
                        weighted_graph_path = BFS_Algorithm.BFS_for_weighted(starting_node, ending_node);
                        Weighted_board.Invalidate();
                    }
                    else
                    {
                        MessageBox.Show("Please enter valid start and end node numbers.", "Invalid Nodes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter valid start and end node numbers.", "Invalid Nodes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else 
            {
                algorithm_picker = algorithm_picked.BFS;
                Timer_algorithm_tick.Start();
            }
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
            if (weighted_graph_selected)
            {
                if (int.TryParse(textBox_StartingNode.Text, out int startNumber) && int.TryParse(textBox_EndingNode.Text, out int endNumber))
                {
                    Node starting_node = FindNodeByNumber(startNumber);
                    Node ending_node = FindNodeByNumber(endNumber);

                    if (starting_node != null && ending_node != null)
                    {
                        weighted_graph_path = DFS_Algorithm.DFS_for_weighted(starting_node, ending_node);
                        Weighted_board.Invalidate();
                    }
                    else
                    {
                        MessageBox.Show("Please enter valid start and end node numbers.", "Invalid Nodes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter valid start and end node numbers.", "Invalid Nodes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                algorithm_picker = algorithm_picked.DFS;
                Timer_algorithm_tick.Start();
            }
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
            if (weighted_graph_selected)
            {
                if (int.TryParse(textBox_StartingNode.Text, out int startNumber) && int.TryParse(textBox_EndingNode.Text, out int endNumber))
                {
                    Node starting_node = FindNodeByNumber(startNumber);
                    Node ending_node = FindNodeByNumber(endNumber);

                    if (starting_node != null && ending_node != null)
                    {
                        weighted_graph_path = Dijkstra_Algorithm.Dijkstra_for_weighted(starting_node, ending_node, nodes);
                        Weighted_board.Invalidate();
                    }
                    else
                    {
                        MessageBox.Show("Please enter valid start and end node numbers.", "Invalid Nodes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter valid start and end node numbers.", "Invalid Nodes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                algorithm_picker = algorithm_picked.Dijkstra;
                Timer_algorithm_tick.Start();
            }
        }

        /// <summary>
        /// Calls the AStar algorithm if the "A Star" button is clicked
        /// </summary>
        private void Button_AStar_Click(object sender, EventArgs e)
        {
            AStar_Algorithm = new AStarAlgorithm(this);
            if (weighted_graph_selected)
            {
                if (int.TryParse(textBox_StartingNode.Text, out int startNumber) && int.TryParse(textBox_EndingNode.Text, out int endNumber))
                {
                    Node starting_node = FindNodeByNumber(startNumber);
                    Node ending_node = FindNodeByNumber(endNumber);

                    if (starting_node != null && ending_node != null)
                    {
                        weighted_graph_path = AStar_Algorithm.AStar_for_weighted(starting_node, ending_node, nodes);
                        Weighted_board.Invalidate();
                    }
                    else
                    {
                        MessageBox.Show("Please enter valid start and end node numbers.", "Invalid Nodes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter valid start and end node numbers.", "Invalid Nodes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                algorithm_picker = algorithm_picked.AStar;
                Timer_algorithm_tick.Start();
            }
        }

        /// <summary>
        /// Adds an edge between the two specified nodes and adds a wiehgt to them
        /// </summary>
        private void Button_add_edge_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox_From.Text, out int source_num) && int.TryParse(textBox_To.Text, out int target_num))
            {
                Node source_node = FindNodeByNumber(source_num);
                Node target_node = FindNodeByNumber(target_num);

                if (source_node != null && target_node != null)
                {
                    if (int.TryParse(textBox__weight.Text, out int weight))
                    {
                        source_node.AddEdge(target_node, weight);
                        target_node.AddEdge(source_node, weight);
                        Weighted_board.Invalidate();
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid weight.", "Invalid Weight", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter valid source and target node numbers.", "Invalid Nodes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Please enter valid source and target node numbers.", "Invalid Nodes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Adds a node to tohe board
        /// </summary>
        private void Weighted_board_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                current_number++;
                Node node = new Node(e.Location, current_number);
                nodes.Add(node);
                Weighted_board.Invalidate();
            }
        }

        /// <summary>
        /// Paints the board according to the user specified input
        /// </summary>
        private void Weighted_board_Paint(object sender, PaintEventArgs e)
        {
            foreach (Node node in nodes)
            {
                node.Draw(e.Graphics);
                foreach (Edge edge in node.Edges)
                {
                    Color edge_color = weighted_graph_path.Contains(node) && weighted_graph_path.Contains(edge.Target) ? Color.Yellow : Color.Black;
                    using (Pen edge_pen = new Pen(edge_color))
                    {
                        e.Graphics.DrawLine(edge_pen, node.Location, edge.Target.Location);
                    }
                    Point mid_point = new Point((node.Location.X + edge.Target.Location.X) / 2, (node.Location.Y + edge.Target.Location.Y) / 2);
                    e.Graphics.DrawString(edge.Weight.ToString(), node.node_number_font, node.node_number_brush, mid_point, node.node_number_format);
                }
            }
        }

        /// <summary>
        /// Function for finding the node behind the number specified
        /// </summary>
        /// <param name="number"></param>
        /// <returns>the node found</returns>
        private Node FindNodeByNumber(int number)
        {
            foreach (Node node in nodes)
            {
                if (node.Number == number)
                {
                    return node;
                }
            }
            return null;
        }

        /// <summary>
        /// Specifies which algorithm should the program use
        /// </summary>

        private void Button_switch_Click(object sender, EventArgs e)
        {
            if (weighted_graph_selected)
            {
                weighted_graph_selected = false;
                Label_switch_state.Text = "Not weighted";
            }
            else
            {
                weighted_graph_selected = true;
                Label_switch_state.Text = "Weighted";
            }
        }

        /// <summary>
        /// Resets the  unweighted graph
        /// </summary>
        private void Button_reset_weighted_Click(object sender, EventArgs e)
        {
            nodes.Clear();
            current_number = 0;
            weighted_graph_path.Clear();
            textBox_From.Clear();
            textBox_To.Clear();
            textBox__weight.Clear();
            textBox_StartingNode.Clear();
            textBox_EndingNode.Clear();
            Weighted_board.Invalidate();
        }
    }




}