namespace Zapoctak
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
            Board = new PictureBox();
            Button_reset = new Button();
            Button_BFS = new Button();
            Timer_algorithm_tick = new System.Windows.Forms.Timer(components);
            ScrollBar_Algorithm_Speed = new HScrollBar();
            ((System.ComponentModel.ISupportInitialize)Board).BeginInit();
            SuspendLayout();
            // 
            // Board
            // 
            Board.Location = new Point(175, 12);
            Board.Name = "Board";
            Board.Size = new Size(500, 500);
            Board.TabIndex = 0;
            Board.TabStop = false;
            Board.Paint += Board_Paint;
            Board.MouseDown += Board_MouseDown;
            Board.MouseMove += Board_MouseMove;
            // 
            // Button_reset
            // 
            Button_reset.Location = new Point(797, 47);
            Button_reset.Name = "Button_reset";
            Button_reset.Size = new Size(112, 34);
            Button_reset.TabIndex = 1;
            Button_reset.Text = "Reset";
            Button_reset.UseVisualStyleBackColor = true;
            Button_reset.Click += Button_reset_Click;
            // 
            // Button_BFS
            // 
            Button_BFS.Location = new Point(797, 150);
            Button_BFS.Name = "Button_BFS";
            Button_BFS.Size = new Size(112, 34);
            Button_BFS.TabIndex = 2;
            Button_BFS.Text = "BFS";
            Button_BFS.UseVisualStyleBackColor = true;
            Button_BFS.Click += Button_BFS_Click;
            // 
            // Timer_algorithm_tick
            // 
            Timer_algorithm_tick.Interval = 5000;
            Timer_algorithm_tick.Tick += Timer_Algorithm_Tick_Tick;
            // 
            // ScrollBar_Algorithm_Speed
            // 
            ScrollBar_Algorithm_Speed.Location = new Point(702, 257);
            ScrollBar_Algorithm_Speed.Name = "ScrollBar_Algorithm_Speed";
            ScrollBar_Algorithm_Speed.Size = new Size(187, 46);
            ScrollBar_Algorithm_Speed.TabIndex = 3;
            ScrollBar_Algorithm_Speed.ValueChanged += ScrollBar_Algorithm_Speed_ValueChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1034, 528);
            Controls.Add(ScrollBar_Algorithm_Speed);
            Controls.Add(Button_BFS);
            Controls.Add(Button_reset);
            Controls.Add(Board);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)Board).EndInit();
            ResumeLayout(false);
        }

        #endregion

        public PictureBox Board;
        private Button Button_reset;
        private Button Button_BFS;
        private System.Windows.Forms.Timer Timer_algorithm_tick;
        private HScrollBar ScrollBar_Algorithm_Speed;
    }
}