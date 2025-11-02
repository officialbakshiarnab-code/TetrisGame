using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TetrisGame
{
    public partial class Form1 : Form
    {
        const int BlockSize = 30;
        const int GridWidth = 13;
        const int GridHeight = 26;

        private System.Windows.Forms.Timer gameTimer = new System.Windows.Forms.Timer();
        private Shape currentShape;
        private List<Shape> shapes;
        private int[,] grid = new int[GridHeight, GridWidth];
        private bool isGameOver = false;
        private int score = 0;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.KeyPreview = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            this.Focus();

            shapes = new List<Shape>
            {
                new Shape(new int[,] {{1,1,1,1}}, Color.Cyan),
                new Shape(new int[,] {{1,1},{1,1}}, Color.Yellow),
                new Shape(new int[,] {{0,1,0},{1,1,1}}, Color.Purple),
                new Shape(new int[,] {{1,0,0},{1,1,1}}, Color.Blue),
                new Shape(new int[,] {{0,0,1},{1,1,1}}, Color.Orange),
                new Shape(new int[,] {{0,1,1},{1,1,0}}, Color.Green),
                new Shape(new int[,] {{1,1,0},{0,1,1}}, Color.Red)
            };

            gameTimer.Interval = 500;
            gameTimer.Tick += GameTick;
            gameTimer.Start();
        }

        private void GameTick(object sender, EventArgs e)
        {
            if (isGameOver) return;

            if (currentShape == null)
                currentShape = GetRandomShape();

            if (!IsCollision(currentShape, 0, 1))
            {
                currentShape.Y++;
            }
            else
            {
                for (int y = 0; y < currentShape.Matrix.GetLength(0); y++)
                {
                    for (int x = 0; x < currentShape.Matrix.GetLength(1); x++)
                    {
                        if (currentShape.Matrix[y, x] == 1)
                        {
                            int gridX = currentShape.X + x;
                            int gridY = currentShape.Y + y;
                            if (gridY >= 0 && gridY < GridHeight && gridX >= 0 && gridX < GridWidth)
                                grid[gridY, gridX] = 1;
                        }
                    }
                }

                ClearLines();

                for (int x = 0; x < GridWidth; x++)
                {
                    if (grid[0, x] == 1)
                    {
                        isGameOver = true;
                        gameTimer.Stop();
                        MessageBox.Show("Game Over!");
                        break;
                    }
                }

                currentShape = GetRandomShape();
            }

            Invalidate();
        }

        private Shape GetRandomShape()
        {
            Random rand = new Random();
            int index = rand.Next(shapes.Count);
            return new Shape(shapes[index].Matrix, shapes[index].Color);
        }

        private bool IsCollision(Shape shape, int offsetX, int offsetY)
        {
            for (int y = 0; y < shape.Matrix.GetLength(0); y++)
            {
                for (int x = 0; x < shape.Matrix.GetLength(1); x++)
                {
                    if (shape.Matrix[y, x] == 1)
                    {
                        int newX = shape.X + x + offsetX;
                        int newY = shape.Y + y + offsetY;

                        if (newX < 0 || newX >= GridWidth || newY >= GridHeight)
                            return true;

                        if (newY >= 0 && grid[newY, newX] == 1)
                            return true;
                    }
                }
            }
            return false;
        }

        private void ClearLines()
        {
            for (int y = GridHeight - 1; y >= 0; y--)
            {
                bool fullLine = true;
                for (int x = 0; x < GridWidth; x++)
                {
                    if (grid[y, x] == 0)
                    {
                        fullLine = false;
                        break;
                    }
                }

                if (fullLine)
                {
                    // Shift all rows above down by one
                    for (int row = y; row > 0; row--)
                    {
                        for (int col = 0; col < GridWidth; col++)
                        {
                            grid[row, col] = grid[row - 1, col];
                        }
                    }

                    // Clear the top row
                    for (int col = 0; col < GridWidth; col++)
                    {
                        grid[0, col] = 0;
                    }

                    score += 100;

                    // Recheck the same row index after shifting
                    y++;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Calculate horizontal offset to center the grid
            int gridOffsetX = (ClientSize.Width - GridWidth * BlockSize) / 2;
            int gridWidthPixels = GridWidth * BlockSize;
            int gridHeightPixels = GridHeight * BlockSize;

            // Draw placed blocks
            for (int y = 0; y < GridHeight; y++)
            {
                for (int x = 0; x < GridWidth; x++)
                {
                    if (grid[y, x] != 0)
                    {
                        Brush brush = Brushes.Blue; // You can customize per shape
                        g.FillRectangle(brush,
                            gridOffsetX + x * BlockSize,
                            y * BlockSize,
                            BlockSize,
                            BlockSize);
                    }
                }
            }

            // Draw current falling shape
            if (currentShape != null)
            {
                for (int y = 0; y < currentShape.Matrix.GetLength(0); y++)
                {
                    for (int x = 0; x < currentShape.Matrix.GetLength(1); x++)
                    {
                        if (currentShape.Matrix[y, x] == 1)
                        {
                            g.FillRectangle(new SolidBrush(currentShape.Color),
                                gridOffsetX + (currentShape.X + x) * BlockSize,
                                (currentShape.Y + y) * BlockSize,
                                BlockSize,
                                BlockSize);
                        }
                    }
                }
            }

            // Draw outer border around the grid
            g.DrawRectangle(Pens.Black, gridOffsetX, 0, gridWidthPixels, gridHeightPixels);

            // Draw score at bottom-left
            g.DrawString(lblScore.Text = $"Score: {score}", new Font("Arial", 16), Brushes.Black, new PointF(10, 865));
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (isGameOver || currentShape == null) return base.ProcessCmdKey(ref msg, keyData);

            switch (keyData)
            {
                case Keys.Left:
                case Keys.A:
                    currentShape.X--;
                    if (IsCollision(currentShape, 0, 0))
                        currentShape.X++;
                    break;

                case Keys.Right:
                case Keys.D:
                    currentShape.X++;
                    if (IsCollision(currentShape, 0, 0))
                        currentShape.X--;
                    break;

                case Keys.Down:
                case Keys.S:
                    currentShape.Y++;
                    if (IsCollision(currentShape, 0, 0))
                        currentShape.Y--;
                    break;

                case Keys.Up:
                case Keys.W:
                    currentShape.Rotate();
                    if (IsCollision(currentShape, 0, 0))
                    {
                        currentShape.Rotate();
                        currentShape.Rotate();
                        currentShape.Rotate();
                    }
                    break;
                case Keys.Tab:
                    MirrorShape(currentShape);
                    if (IsCollision(currentShape, 0, 0))
                    {
                        // Undo mirror if it causes collision
                        MirrorShape(currentShape);
                    }
                    break;
            }

            Invalidate();
            return true;
        }

        private void MirrorShape(Shape shape)
        {
            int rows = shape.Matrix.GetLength(0);
            int cols = shape.Matrix.GetLength(1);
            int[,] mirrored = new int[rows, cols];

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    mirrored[y, x] = shape.Matrix[y, cols - 1 - x];
                }
            }

            shape.Matrix = mirrored;
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            grid = new int[GridHeight, GridWidth];
            currentShape = null;
            score = 0;
            isGameOver = false;
            gameTimer.Start();
            this.Focus();
            Invalidate();
        }
    }
}