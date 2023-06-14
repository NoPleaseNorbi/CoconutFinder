namespace PathFinder
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Timer_algorithm_tick = new System.Windows.Forms.Timer(components);
            Button_reset = new Button();
            Button_DFS = new Button();
            Button_BFS = new Button();
            Board = new PictureBox();
            Button_Random_maze_generator = new Button();
            ScrollBar_Algorithm_Speed = new HScrollBar();
            label1 = new Label();
            Label_Information = new Label();
            Button_Dijkstra = new Button();
            Button_A_Star = new Button();
            Weighted_board = new PictureBox();
            textBox_From = new TextBox();
            textBox_To = new TextBox();
            textBox_EndingNode = new TextBox();
            textBox_StartingNode = new TextBox();
            textBox__weight = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            Button_add_edge = new Button();
            Button_reset_weighted = new Button();
            Button_switch = new Button();
            Label_switch_state = new Label();
            ((System.ComponentModel.ISupportInitialize)Board).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Weighted_board).BeginInit();
            SuspendLayout();
            // 
            // Timer_algorithm_tick
            // 
            Timer_algorithm_tick.Interval = 5000;
            Timer_algorithm_tick.Tick += Timer_Algorithm_Tick_Tick;
            // 
            // Button_reset
            // 
            Button_reset.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button_reset.Location = new Point(768, 451);
            Button_reset.Name = "Button_reset";
            Button_reset.Size = new Size(210, 34);
            Button_reset.TabIndex = 1;
            Button_reset.Text = "Reset";
            Button_reset.UseVisualStyleBackColor = true;
            Button_reset.Click += Button_reset_Click;
            // 
            // Button_DFS
            // 
            Button_DFS.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button_DFS.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Button_DFS.Cursor = Cursors.Hand;
            Button_DFS.Location = new Point(704, 131);
            Button_DFS.Name = "Button_DFS";
            Button_DFS.Size = new Size(169, 88);
            Button_DFS.TabIndex = 4;
            Button_DFS.Text = "DFS";
            Button_DFS.UseVisualStyleBackColor = true;
            Button_DFS.Click += Button_DFS_Click;
            // 
            // Button_BFS
            // 
            Button_BFS.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button_BFS.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Button_BFS.Cursor = Cursors.Hand;
            Button_BFS.Location = new Point(704, 29);
            Button_BFS.Name = "Button_BFS";
            Button_BFS.Size = new Size(169, 86);
            Button_BFS.TabIndex = 2;
            Button_BFS.Text = "BFS";
            Button_BFS.UseVisualStyleBackColor = true;
            Button_BFS.Click += Button_BFS_Click;
            // 
            // Board
            // 
            Board.Location = new Point(30, 12);
            Board.Name = "Board";
            Board.Size = new Size(500, 500);
            Board.TabIndex = 0;
            Board.TabStop = false;
            Board.Paint += Board_Paint;
            Board.MouseDown += Board_MouseDown;
            Board.MouseMove += Board_MouseMove;
            // 
            // Button_Random_maze_generator
            // 
            Button_Random_maze_generator.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Button_Random_maze_generator.Location = new Point(768, 395);
            Button_Random_maze_generator.Name = "Button_Random_maze_generator";
            Button_Random_maze_generator.Size = new Size(210, 34);
            Button_Random_maze_generator.TabIndex = 6;
            Button_Random_maze_generator.Text = "Generate Random Maze";
            Button_Random_maze_generator.UseVisualStyleBackColor = true;
            Button_Random_maze_generator.Click += Button_Random_maze_generator_Click;
            // 
            // ScrollBar_Algorithm_Speed
            // 
            ScrollBar_Algorithm_Speed.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ScrollBar_Algorithm_Speed.Location = new Point(704, 316);
            ScrollBar_Algorithm_Speed.Maximum = 500;
            ScrollBar_Algorithm_Speed.Name = "ScrollBar_Algorithm_Speed";
            ScrollBar_Algorithm_Speed.RightToLeft = RightToLeft.No;
            ScrollBar_Algorithm_Speed.Size = new Size(347, 46);
            ScrollBar_Algorithm_Speed.TabIndex = 3;
            ScrollBar_Algorithm_Speed.ValueChanged += ScrollBar_Algorithm_Speed_ValueChanged;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(704, 258);
            label1.Name = "label1";
            label1.Size = new Size(347, 22);
            label1.TabIndex = 7;
            label1.Text = "Change the speed of the visualization";
            label1.TextAlign = ContentAlignment.TopCenter;
            // 
            // Label_Information
            // 
            Label_Information.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Label_Information.AutoSize = true;
            Label_Information.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Label_Information.Location = new Point(768, 513);
            Label_Information.Name = "Label_Information";
            Label_Information.Size = new Size(0, 22);
            Label_Information.TabIndex = 8;
            Label_Information.TextAlign = ContentAlignment.TopCenter;
            // 
            // Button_Dijkstra
            // 
            Button_Dijkstra.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button_Dijkstra.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Button_Dijkstra.Cursor = Cursors.Hand;
            Button_Dijkstra.Location = new Point(882, 29);
            Button_Dijkstra.Name = "Button_Dijkstra";
            Button_Dijkstra.Size = new Size(169, 86);
            Button_Dijkstra.TabIndex = 9;
            Button_Dijkstra.Text = "Dijkstra";
            Button_Dijkstra.UseVisualStyleBackColor = true;
            Button_Dijkstra.Click += Button_Dijkstra_Click;
            // 
            // Button_A_Star
            // 
            Button_A_Star.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button_A_Star.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Button_A_Star.Cursor = Cursors.Hand;
            Button_A_Star.Location = new Point(882, 133);
            Button_A_Star.Name = "Button_A_Star";
            Button_A_Star.Size = new Size(169, 86);
            Button_A_Star.TabIndex = 10;
            Button_A_Star.Text = "A Star";
            Button_A_Star.UseVisualStyleBackColor = true;
            Button_A_Star.Click += Button_AStar_Click;
            // 
            // Weighted_board
            // 
            Weighted_board.Location = new Point(568, 538);
            Weighted_board.Name = "Weighted_board";
            Weighted_board.Size = new Size(500, 500);
            Weighted_board.TabIndex = 11;
            Weighted_board.TabStop = false;
            Weighted_board.Paint += Weighted_board_Paint;
            Weighted_board.MouseDown += Weighted_board_MouseDown;
            // 
            // textBox_From
            // 
            textBox_From.Location = new Point(30, 596);
            textBox_From.Name = "textBox_From";
            textBox_From.Size = new Size(150, 31);
            textBox_From.TabIndex = 12;
            // 
            // textBox_To
            // 
            textBox_To.Location = new Point(30, 675);
            textBox_To.Name = "textBox_To";
            textBox_To.Size = new Size(150, 31);
            textBox_To.TabIndex = 13;
            // 
            // textBox_EndingNode
            // 
            textBox_EndingNode.Location = new Point(318, 675);
            textBox_EndingNode.Name = "textBox_EndingNode";
            textBox_EndingNode.Size = new Size(150, 31);
            textBox_EndingNode.TabIndex = 14;
            // 
            // textBox_StartingNode
            // 
            textBox_StartingNode.Location = new Point(318, 596);
            textBox_StartingNode.Name = "textBox_StartingNode";
            textBox_StartingNode.Size = new Size(150, 31);
            textBox_StartingNode.TabIndex = 15;
            // 
            // textBox__weight
            // 
            textBox__weight.Location = new Point(30, 751);
            textBox__weight.Name = "textBox__weight";
            textBox__weight.Size = new Size(150, 31);
            textBox__weight.TabIndex = 16;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(30, 568);
            label2.Name = "label2";
            label2.Size = new Size(54, 25);
            label2.TabIndex = 17;
            label2.Text = "From";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(318, 568);
            label3.Name = "label3";
            label3.Size = new Size(119, 25);
            label3.TabIndex = 18;
            label3.Text = "Starting node";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(30, 647);
            label4.Name = "label4";
            label4.Size = new Size(30, 25);
            label4.TabIndex = 19;
            label4.Text = "To";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(318, 647);
            label5.Name = "label5";
            label5.Size = new Size(113, 25);
            label5.TabIndex = 20;
            label5.Text = "Ending node";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(30, 723);
            label6.Name = "label6";
            label6.Size = new Size(68, 25);
            label6.TabIndex = 21;
            label6.Text = "Weight";
            // 
            // Button_add_edge
            // 
            Button_add_edge.Location = new Point(30, 859);
            Button_add_edge.Name = "Button_add_edge";
            Button_add_edge.Size = new Size(150, 59);
            Button_add_edge.TabIndex = 22;
            Button_add_edge.Text = "Add edge";
            Button_add_edge.UseVisualStyleBackColor = true;
            Button_add_edge.Click += Button_add_edge_Click;
            // 
            // Button_reset_weighted
            // 
            Button_reset_weighted.Location = new Point(318, 859);
            Button_reset_weighted.Name = "Button_reset_weighted";
            Button_reset_weighted.Size = new Size(150, 59);
            Button_reset_weighted.TabIndex = 23;
            Button_reset_weighted.Text = "Reset";
            Button_reset_weighted.UseVisualStyleBackColor = true;
            Button_reset_weighted.Click += Button_reset_weighted_Click;
            // 
            // Button_switch
            // 
            Button_switch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Button_switch.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Button_switch.Cursor = Cursors.Hand;
            Button_switch.Location = new Point(551, 395);
            Button_switch.Name = "Button_switch";
            Button_switch.Size = new Size(127, 67);
            Button_switch.TabIndex = 24;
            Button_switch.Text = "Switch Algorithm";
            Button_switch.UseVisualStyleBackColor = true;
            Button_switch.Click += Button_switch_Click;
            // 
            // Label_switch_state
            // 
            Label_switch_state.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Label_switch_state.AutoSize = true;
            Label_switch_state.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Label_switch_state.Location = new Point(551, 484);
            Label_switch_state.Name = "Label_switch_state";
            Label_switch_state.Size = new Size(131, 22);
            Label_switch_state.TabIndex = 25;
            Label_switch_state.Text = "Not weighted";
            Label_switch_state.TextAlign = ContentAlignment.TopCenter;
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.None;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1080, 1050);
            Controls.Add(Label_switch_state);
            Controls.Add(Button_switch);
            Controls.Add(Button_reset_weighted);
            Controls.Add(Button_add_edge);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBox__weight);
            Controls.Add(textBox_StartingNode);
            Controls.Add(textBox_EndingNode);
            Controls.Add(textBox_To);
            Controls.Add(textBox_From);
            Controls.Add(Weighted_board);
            Controls.Add(Button_A_Star);
            Controls.Add(Button_Dijkstra);
            Controls.Add(Label_Information);
            Controls.Add(label1);
            Controls.Add(ScrollBar_Algorithm_Speed);
            Controls.Add(Button_DFS);
            Controls.Add(Button_BFS);
            Controls.Add(Button_reset);
            Controls.Add(Board);
            Controls.Add(Button_Random_maze_generator);
            Name = "Form1";
            Text = "CoconutFinder";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)Board).EndInit();
            ((System.ComponentModel.ISupportInitialize)Weighted_board).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        public System.Windows.Forms.Timer Timer_algorithm_tick;
        private Button Button_reset;
        private Button Button_DFS;
        private Button Button_BFS;
        public PictureBox Board;
        private Button Button_Random_maze_generator;
        private HScrollBar ScrollBar_Algorithm_Speed;
        private Label label1;
        public Label Label_Information;
        private Button Button_Dijkstra;
        private Button Button_A_Star;
        public PictureBox Weighted_board;
        private TextBox textBox_From;
        private TextBox textBox_To;
        private TextBox textBox_EndingNode;
        private TextBox textBox_StartingNode;
        private TextBox textBox__weight;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button Button_add_edge;
        private Button Button_reset_weighted;
        private Button Button_switch;
        private Label Label_switch_state;
    }
}