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
            ((System.ComponentModel.ISupportInitialize)Board).BeginInit();
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
            Button_reset.Location = new Point(612, 396);
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
            Button_DFS.Location = new Point(632, 121);
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
            Button_BFS.Location = new Point(632, 29);
            Button_BFS.Name = "Button_BFS";
            Button_BFS.Size = new Size(169, 86);
            Button_BFS.TabIndex = 2;
            Button_BFS.Text = "BFS";
            Button_BFS.UseVisualStyleBackColor = true;
            Button_BFS.Click += Button_BFS_Click;
            // 
            // Board
            // 
            Board.Anchor = AnchorStyles.Left;
            Board.Location = new Point(12, 53);
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
            Button_Random_maze_generator.Location = new Point(612, 339);
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
            ScrollBar_Algorithm_Speed.Location = new Point(548, 269);
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
            label1.Location = new Point(548, 237);
            label1.Name = "label1";
            label1.Size = new Size(347, 22);
            label1.TabIndex = 7;
            label1.Text = "Change the speed of the visualization";
            label1.TextAlign = ContentAlignment.TopCenter;
            label1.Click += label1_Click;
            // 
            // Label_Information
            // 
            Label_Information.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            Label_Information.AutoSize = true;
            Label_Information.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
            Label_Information.Location = new Point(612, 479);
            Label_Information.Name = "Label_Information";
            Label_Information.Size = new Size(0, 22);
            Label_Information.TabIndex = 8;
            Label_Information.TextAlign = ContentAlignment.TopCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(924, 595);
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
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Timer Timer_algorithm_tick;
        private Button Button_reset;
        private Button Button_DFS;
        private Button Button_BFS;
        public PictureBox Board;
        private Button Button_Random_maze_generator;
        private HScrollBar ScrollBar_Algorithm_Speed;
        private Label label1;
        public Label Label_Information;
    }
}