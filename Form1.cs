using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TetrisGame
{
    public partial class Form1 : Form
    {
        #region Declarations

        const int BlockSize = 30;
        const int GridWidth = 13;
        const int GridHeight = 26;

        private System.Windows.Forms.Timer gameTimer = new System.Windows.Forms.Timer();
        private Shape currentShape;
        private List<Shape> shapes;
        private int[,] grid = new int[GridHeight, GridWidth];
        private bool isGameOver = false;
        private int score = 0;
        private Shape nextShape;

        private List<int> flashingRows = new List<int>();
        private int flashCounter = 0;
        private System.Windows.Forms.Timer flashTimer = new System.Windows.Forms.Timer();

        private bool isPaused = false;

        #endregion

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
            if (isGameOver || isPaused) return;

            // Spawn shape from preview or generate first
            if (currentShape == null)
            {
                currentShape = nextShape ?? GetRandomShape();
                nextShape = GetRandomShape();
            }

            // Move shape down if no collision
            if (!IsCollision(currentShape, 0, 1))
            {
                currentShape.Y++;
            }
            else
            {
                // Lock shape into grid
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

                // Check for game over
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

                // Move next shape into play
                currentShape = nextShape;
                nextShape = GetRandomShape();
            }

            Invalidate(); // Redraw
        }

        private Shape GetRandomShape()
        {
            Random rand = new Random();
            int index = rand.Next(shapes.Count);
            Shape shape = new Shape(shapes[index].Matrix, shapes[index].Color);
            shape.X = (GridWidth - shape.Matrix.GetLength(1)) / 2;
            shape.Y = 0;
            return shape;
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
            flashingRows.Clear();
            int linesCleared = 0;

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
                    flashingRows.Add(y);
                    linesCleared++;
                }
            }

            if (linesCleared > 0)
            {
                flashCounter = 0;
                flashTimer.Interval = 100;
                flashTimer.Tick += FlashTick;
                flashTimer.Start();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

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
                        bool isFlashing = flashingRows.Contains(y) && flashCounter % 2 == 0;
                        Brush brush = isFlashing ? Brushes.White : Brushes.Blue;

                        g.FillRectangle(brush,
                            gridOffsetX + x * BlockSize,
                            y * BlockSize,
                            BlockSize, BlockSize);
                    }
                }
            }

            // Draw ghost piece
            if (currentShape != null)
            {
                Shape ghost = new Shape(currentShape.Matrix, Color.FromArgb(80, currentShape.Color));
                ghost.X = currentShape.X;
                ghost.Y = currentShape.Y;

                while (!IsCollision(ghost, 0, 1))
                {
                    ghost.Y++;
                }

                for (int y = 0; y < ghost.Matrix.GetLength(0); y++)
                {
                    for (int x = 0; x < ghost.Matrix.GetLength(1); x++)
                    {
                        if (ghost.Matrix[y, x] == 1)
                        {
                            g.FillRectangle(new SolidBrush(ghost.Color),
                                gridOffsetX + (ghost.X + x) * BlockSize,
                                (ghost.Y + y) * BlockSize,
                                BlockSize, BlockSize);
                        }
                    }
                }
            }

            // Draw current shape
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
                                BlockSize, BlockSize);
                        }
                    }
                }
            }

            // Draw grid border
            using (Pen roundedBorder = new Pen(Color.Black, 1))
            using (GraphicsPath path = new GraphicsPath())
            {
                int radius = 20;
                Rectangle rect = new Rectangle(gridOffsetX, 0, gridWidthPixels, gridHeightPixels);
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
                path.CloseFigure();
                g.DrawPath(roundedBorder, path);
            }

            // Draw score
            g.DrawString(lblScore.Text = $"Score: {score}", new Font("Arial", 16), Brushes.Black, new PointF(10, 865));

            // Draw next piece preview
            if (nextShape != null)
            {
                int previewX = ClientSize.Width - 130;
                int previewY = 10;

                g.DrawString("Next:", new Font("Arial", 12), Brushes.Black, new PointF(previewX, previewY));

                for (int y = 0; y < nextShape.Matrix.GetLength(0); y++)
                {
                    for (int x = 0; x < nextShape.Matrix.GetLength(1); x++)
                    {
                        if (nextShape.Matrix[y, x] == 1)
                        {
                            g.FillRectangle(new SolidBrush(nextShape.Color),
                                previewX + x * BlockSize,
                                previewY + 20 + y * BlockSize,
                                BlockSize, BlockSize);
                        }
                    }
                }
            }

            // Draw pause overlay
            if (isPaused)
            {
                using (Brush overlay = new SolidBrush(Color.FromArgb(180, Color.Black)))
                {
                    g.FillRectangle(overlay, 0, 0, ClientSize.Width, ClientSize.Height);
                }

                string pauseText = "PAUSED";
                Font pauseFont = new Font("Arial", 32, FontStyle.Bold);
                SizeF textSize = g.MeasureString(pauseText, pauseFont);
                g.DrawString(pauseText, pauseFont, Brushes.White,
                    (ClientSize.Width - textSize.Width) / 2,
                    (ClientSize.Height - textSize.Height) / 2);
            }
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
                case Keys.P:
                    isPaused = !isPaused;
                    if (isPaused)
                    {
                        gameTimer.Stop();
                        lblScore.Text = "Paused";
                    }
                    else
                    {
                        gameTimer.Start();
                        lblScore.Text = $"Score: {score}";
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

        private void FlashTick(object sender, EventArgs e)
        {
            flashCounter++;
            if (flashCounter >= 6)
            {
                flashTimer.Stop();
                flashTimer.Tick -= FlashTick;

                foreach (int y in flashingRows)
                {
                    for (int row = y; row > 0; row--)
                    {
                        for (int col = 0; col < GridWidth; col++)
                        {
                            grid[row, col] = grid[row - 1, col];
                        }
                    }
                    for (int col = 0; col < GridWidth; col++)
                    {
                        grid[0, col] = 0;
                    }
                }

                score += flashingRows.Count * 100 + (flashingRows.Count - 1) * 50;
                flashingRows.Clear();
            }

            Invalidate();
        }
    }
}