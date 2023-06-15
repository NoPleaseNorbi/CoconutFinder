# Main classes used in the program
This it he programming documentation for the CoconutFinder application written in C#. This documentation can be used alongside the doxygen documentation, which can be generated using the instructions in the readme file. 

## Location and other not so important things
Every file of this program is located inside the `PathFinder` folder. The program was written in C# and uses WinForms as its main framework. The user can specify if he wants to compute the algorithms for weighted or unweighted graphs. I implemented 4 different algorithms, DFS, BFS, Dijkstra's and A star to accomodate the needs of a basic user who wants to see some algorithms in action. The user can learn more about the program using the `MANUAL.pdf` attached to the root directory of this project. 

## Edge Class
The Edge class is used to define the connections between nodes in a graph. Each edge has a target node and a weight associated with it.

1. Constructors
- `Edge(Node target, int weight)`: Initializes a new instance of the Edge class with the specified target node and weight.
2. Properties
- `Target`: Gets the target node of the edge.
- `Weight`: Gets the weight of the edge.


## Node Class
The Node class represents a node in a weighted graph. It stores information such as its location, number, and the edges connected to it. The class provides methods to add edges, check if a point is contained within the node, and draw the node on a Graphics object.

1. Fields
- `node_number_font`: Represents the font used to display the node number.
- `node_number_brush`: Represents the brush used to draw the node number.
- `node_number_format`: Represents the string format used to align the node number.

2. Properties
- `Location`: Gets the location of the node.
- `Number`: Gets the number associated with the node.
- `Edges`: Gets the list of edges connected to the node.

3. Constructors
- `Node(Point location, int number)`: Initializes a new instance of the Node class with the specified location and number.

4. Methods
- `AddEdge(Node target, int weight)`: Adds an edge to the node connecting it to the specified target node with the given weight.

- `ContainsPoint(Point point)`: Determines if the node contains the specified point.
- `Draw(Graphics graphics)`: Draws the node on the graphics object.

## BFSAlgorithm Class
The BFSAlgorithm class provides methods to run the BFS algorithm on a grid, trace the shortest path, and perform the BFS algorithm on a weighted graph. It also includes utility methods to determine if the algorithm has finished and to retrieve the edge between two nodes. It begins at the root of the graph and investigates all nodes at the current depth level before moving on to nodes at the next depth level

1. Fields
- `form`: Represents the main form of the application.
- `distances`: Stores the distances from the starting point to each cell in the grid.
- `visited`: Keeps track of visited cells in the grid.
- `finished`: Indicates whether the algorithm has finished.
- `rows_number`: The number of rows in the grid.
- `cols_number`: The number of columns in the grid.
- `queue`: Stores the cells to be processed in the BFS algorithm.
- `starting_point`: The starting point in the grid.
- `ending_point`: The ending point in the grid.

2. Constructors
- `BFSAlgorithm(Form1 form)`: Initializes a new instance of the BFSAlgorithm class with the specified main form.

3. Methods
- `Finished()`: Determines if the algorithm has finished.
- `RunBFS()`: Runs a single step of the BFS algorithm.
- `TraceShortestPath()`: Traces the shortest path from the starting point to the ending point.
- `BFS_for_weighted(Node starting_node, Node ending_node)`: Performs the BFS algorithm on a weighted graph to find the shortest path between the starting node and the ending node.
- `GetEdgeBetweenNodes`(Node source, Node target): Retrieves the edge between two nodes for the weighted grpah.


## DFSAlgorithm Class
The DFSAlgorithm class is the main class for the DFS algorithm. It provides methods to run the DFS algorithm on a grid, trace the shortest path, and perform the DFS algorithm on a weighted graph. It also includes utility methods to determine if the algorithm has finished and to retrieve the edge between two nodes. It entails conducting exhaustive searches of all nodes by moving forward if possible and backtracking, if necessary

1. Fields
- `form`: Represents the main form of the application.
- `distances`: Stores the distances from the starting point to each cell in the grid.
- `visited`: Keeps track of visited cells in the grid.
- `finished`: Indicates whether the algorithm has finished.
- `rows_number`: The number of rows in the grid.
- `cols_number`: The number of columns in the grid.
- `stack`: Stores the cells to be processed in the DFS algorithm.
- `starting_point`: The starting point in the grid.
- `ending_point`: The ending point in the grid.

2. Constructors
- `DFSAlgorithm(Form1 form)`: Initializes a new instance of the DFSAlgorithm class with the specified main form.

3. Methods
- `Finished()`: Determines if the algorithm has finished.
- `RunDFS()`: Runs a single step of the DFS algorithm.
- `TraceShortestPath()`: Traces the shortest path from the starting point to the ending point.
- `DFS_for_weighted(Node starting_node, Node ending_node)`: Performs the DFS algorithm on a weighted graph to find the shortest path between the starting node and the ending node.
- `GetEdgeBetweenNodes(Node source, Node target)`: Retrieves the edge between two nodes for the weighted graph.

## AStarAlgorithm class
The main class for the A star algorithm. It can compute the a star algorithm for both weighted and unweighted graphs. A* works by making a lowest-cost path tree from the start node to the target node. What makes A* different and better for many searches is that for each node, A* uses a function f ( n ) f(n) f(n) that gives an estimate of the total cost of a path using that node.

1. Fields

- `form`: Represents the main form of the application.
- `distances`: Stores the distances from the starting point to each cell in the grid.
- `visited`: Keeps track of visited cells in the grid.
- `finished`: Indicates whether the algorithm has finished.
- `rows_number`: The number of rows in the grid.
- `cols_number`: The number of columns in the grid.
- `open_set`: Stores the cells to be processed in the AStar algorithm.
- `starting_point`: The starting point in the grid.
- `ending_point`: The ending point in the grid.
- `came_from`: The nodes where the algorithm came from
- `heuristic`: The grid where we computed the heuristic for the algorithm

2. Constructors
- `AStarAlgorithm(Form1 form)`: Initializes a new instance of the AStarAlgorithm class with the specified main form.

3. Methods
- `RunAStar()`: Runs a single step of the A* algorithm.
- `ExploreNeighbors(Point current_cell)`: Explores the neighbouring cells of the current_cell
- `ReconstructPath()`: Traces the path from the starting point to the ending point.
- `CalculateHeuristic(Point cell)`: Calculates the heuristic using the function f
- `Finished()`: Determines if the algorithm has finished.
- `AStar_for_weighted(Node starting_node, Node ending_node, List<Node> nodes)`: Performs the A* algorithm on a weighted graph to find the shortest path between the starting node and the ending node.
- `GetEdgeBetweenNodes_for_weighted(Node source, Node target)`: Retrieves the edge between two nodes for the weighted graph.
- `GetClosestNode_for_weighted(Dictionary<Node, int> distances, HashSet<Node> unvisitedNodes)`: Gets the closest node for the weighted graph

## DijkstraAlgorithm
This class computes Dijkstra's algorithm for the wighted and unweighted graphs. Dijkstra's Algorithm is a pathfinding algorithm that generates every single route through the graph, and then selects the route that has the lowest overall cost. This works by iteratively calculating a distance for each node in the graph, starting from the start node, and continuing until we reach the end node

1. Fields
- `form`: Represents the main form of the application.
- `distances`: Stores the distances from the starting point to each cell in the grid.
- `visited`: Keeps track of visited cells in the grid.
- `finished`: Indicates whether the algorithm has finished.
- `rows_number`: The number of rows in the grid.
- `cols_number`: The number of columns in the grid.
- `starting_point`: The starting point in the grid.
- `ending_point`: The ending point in the grid.

2. Constructors
- `DijkstraAlgorithm(Form1 form)`: Initializes a new instance of Dijkstra's algorithm class with the specified main form.

3. Methods
- `Finished()`: Determines if the algorithm has finished.
- `RunDijkstra()`: Runs a single step of Dijkstra's algorithm.
- `TraceShortestPath()`: Traces the shortest path from the starting point to the ending point.
- `Dijkstra_for_weighted(Node starting_node, Node ending_node, List<Node> nodes)`: Performs the A* algorithm on a weighted graph to find the shortest path between the starting node and the ending node.

## RandomMaze
This class is responsible for generating so called random mazes. This is achieved by computing a random function for every node, and if the value falls between 0 and 2 the square on the grid will be left empty, if the random function's value falls for 3 the grid's square will be black, meaning it will be an obstacle.

1. Fields
- `form`: Represents the main form of the application.
- `random`: The random function of the class
- `rows_number`: The number of rows in the grid.
- `cols_number`: The number of columns in the grid.
- `starting_point`: The starting point in the grid.
- `ending_point`: The ending point in the grid.

2. Constructors
- `RandomMaze(Form1 form)`: Initializes a new instance of the random maze algorithm class with the specified main form.

3. Methods
- `GenerateRandomMaze()`: Generates a "random" maze, which uses the algorithm already described earlier. It will leave the starting and ending nodes empty.

## Form1
This class puts together the whole application. This is the main form of this application and is responsible for the user interface, which can be seen in the manual or if the user executes the application. This class is responsible also for the responses after a button click or a mouse click. This class basically is the main class of our application. I will not document the main `Program` class becuase it is generated beforehand and it doesn't need to be documented.

1. Fields
- `square_states`: Defines in which state is currently our unweighted graph in the application, about the states the user can read in the doxygen file
- `algorithm_picked`: Defines which algorithm has been picked for the unweighted graph
- `start_point`: The starting point of our algorithms
- `end_point`: The ending point of our algorithms
- `grid`: The grid representing the unweighted graph in the GUI
- `square_size`: The size of the square on our application
- `grid_size`: The size of the grid used in our application
- `BFS_Algorithm`: The class for our BFS algorithm
- `DFS_Algorithm`: The class for our DFS algorithm
- `Dijkstra_Algorithm`: The class for Dijksta's algorithm
- `AStar_Algorithm`: The class for the a star algorithm
- `random_maze`: The class for the random maze algorithm
- `algorithm_picker`: Indicates which algorithm has been picked
- `interval`: The time interval of our animation
- `current_number`: The current number of the node in the weighted graph
- `nodes`: The nodes in the weighted graph
- `weighted_graph_selected`: Indicates which graph is selected
- `weighted_graph_path_edges`: The path computed in the weighted graph
- `starting_node`: The starting node of the weighted graph algorithm 
- `ending_node`: The ending node of the weighted graph algorithm

2. Constructors
- `Form1()`: Initializes a new instance of the Form1 form.

3. Methods
- `Form1_Load(object sender, EventArgs e)`: Loads the form
- `Board_MouseDown(object sender, MouseEventArgs e)`: If the mouse is down in the unweighted graph it adds a square or deletes one based on the user input.
- `Board_MouseMove(object sender, MouseEventArgs e)`: The same principle applies here, just when the mouse is moving
- `Board_Paint(object sender, PaintEventArgs e)`: Repaints the board based on what states are the squares in the unweighted graph
- `Reset_Board()`: Resets the board to it's original form
- `Button_reset_Click(object sender, EventArgs e)`: Handles if the reset button is clicked.
- `Button_BFS_Click(object sender, EventArgs e)`: Calls the BFS algorithm for weighted or unweighted graphs based on the `weighted_graph_selected` boolean
- `Button_DFS_Click(object sender, EventArgs e)`: Calls the DFS algorithm for weighted or unweighted graphs based on the `weighted_graph_selected` boolean
- `Button_Dijkstra_Click(object sender, EventArgs e)`: Calls Dijkstra's algorithm for weighted or unweighted graphs based on the `weighted_graph_selected` boolean
- `Button_AStar_Click(object sender, EventArgs e)`: Calls A star algorithm for weighted or unweighted graphs based on the `weighted_graph_selected` boolean
- `Timer_Algorithm_Tick_Tick(object sender, EventArgs e)`: Defines the tick of the animation, and handles the animation process in one tick which is defined by the `interval` property
- `ScrollBar_Algorithm_Speed_ValueChanged(object sender, EventArgs e)`: Changes the animation speed if the value of the scrollbar is changed
- `Button_Random_maze_generator_Click(object sender, EventArgs e)`: Calls the random maze generator class to generate a random maze on the grid
- `Button_add_edge_Click(object sender, EventArgs e)`: Adds an edge to the weighted graph based on the textbox inputs
- `Weighted_board_MouseDown(object sender, MouseEventArgs e)`: Adds a node to the weighted board
- `Weighted_board_Paint(object sender, PaintEventArgs e)`: Paints the weighted graph's board based on where the user has clicked
- `FindNodeByNumber(int number)`: Finds the location of the node based on the number assigned to them
- `Button_switch_Click(object sender, EventArgs e)`: Switches the algorithms to weighted/unweighted
- `Button_reset_weighted_Click(object sender, EventArgs e)`: Resets the weighted graph's board to empty.