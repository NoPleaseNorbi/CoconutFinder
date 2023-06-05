using System.Windows.Forms;

namespace Zapoctak
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        bool[,] grid = new bool[25, 25];
        int squareSize = 25;
        private void Board_MouseDown(object sender, MouseEventArgs e)
        {
            int row = e.Y / squareSize;
            int col = e.X / squareSize;

            grid[row, col] = true;

            Board.Invalidate();
        }

        private void Board_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int row = e.Y / squareSize;
                int col = e.X / squareSize;

                grid[row, col] = true;
                Board.Invalidate();
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
                    if (grid[row, col])
                    {
                        g.FillRectangle(Brushes.Red, col * squareSize, row * squareSize, squareSize, squareSize);
                    }
                    else
                    {
                        g.FillRectangle(Brushes.White, col * squareSize, row * squareSize, squareSize, squareSize);
                    }
                    g.DrawRectangle(Pens.Black, squareX, squareY, squareSize, squareSize);
                }
            }
        }

        private void Button_reset_Click(object sender, EventArgs e)
        {
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    grid[row, col] = false;
                }
            }
            Board.Invalidate();
        }
    }
}