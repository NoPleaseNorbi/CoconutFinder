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
            Board = new PictureBox();
            Button_reset = new Button();
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
            Button_reset.Location = new Point(797, 46);
            Button_reset.Name = "Button_reset";
            Button_reset.Size = new Size(112, 34);
            Button_reset.TabIndex = 1;
            Button_reset.Text = "Reset";
            Button_reset.UseVisualStyleBackColor = true;
            Button_reset.Click += Button_reset_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1034, 528);
            Controls.Add(Button_reset);
            Controls.Add(Board);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)Board).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox Board;
        private Button Button_reset;
    }
}