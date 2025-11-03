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
        private System.Windows.Forms.Timer flashTimer = new System.Windows.Forms.Timer();

        private bool isPaused = false;
        private int score = 0;
        private int highScore = 0;
        private bool isGameOver = false;
        private bool showGhostPiece = false;
        private bool mirrorEffectEnabled = false;

        private List<Shape> shapes;
        private Shape currentShape;
        private Shape nextShape;
        private int[,] grid = new int[GridHeight, GridWidth];

        private List<int> flashingRows = new List<int>();
        private int flashCounter = 0;

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

            highScore = Properties.Settings.Default.HighScore;

            shapes = new List<Shape>
            {
                new Shape(new int[,] {{1,1,1,1}}, Color.Cyan),
                new Shape(new int[,] {{1,1},{1,1}}, Color.Yellow),
                new Shape(new int[,] {{0,1,0},{1,1,1}}, Color.Chocolate),
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
            lblScore.Text = $"Score: {score}";
            lblHighScore.Text = $"High Score: {highScore}";
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

                // Sort rows from top to bottom
                flashingRows.Sort();

                foreach (int y in flashingRows)
                {
                    DeleteRow(y);
                }

                score += flashingRows.Count * 100 + (flashingRows.Count - 1) * 50;

                if (score > highScore)
                {
                    highScore = score;
                    Properties.Settings.Default.HighScore = highScore;
                    Properties.Settings.Default.Save();
                }

                flashingRows.Clear();
            }

            Invalidate();
        }

        private void DeleteRow(int rowIndex)
        {
            for (int y = rowIndex; y > 0; y--)
            {
                for (int x = 0; x < GridWidth; x++)
                {
                    grid[y, x] = grid[y - 1, x];
                }
            }

            for (int x = 0; x < GridWidth; x++)
            {
                grid[0, x] = 0;
            }
        }

        private void IconSettings_Click(object sender, EventArgs e)
        {
            settingsPanel.Visible = !settingsPanel.Visible;
        }

        private void ChkGhostPiece_Checked(object sender, EventArgs e)
        {
            showGhostPiece = chkGhostPiece.Checked;
            Invalidate();
        }

        private void IconPause_Click(object sender, EventArgs e)
        {
            if (!isPaused)
            {
                isPaused = true;
                gameTimer.Stop();
                lblScore.Text = $"Score: Paused";
                lblHighScore.Text = $"High Score: {highScore}";
                btnRestart.Enabled = false;

                iconPause.Visible = false;
                iconResume.Visible = true;

                Invalidate();
            }
        }

        private void IconResume_Click(object sender, EventArgs e)
        {
            if (isPaused)
            {
                isPaused = false;
                gameTimer.Start();
                lblScore.Text = $"Score: {score}";
                lblHighScore.Text = $"High Score: {highScore}";
                btnRestart.Enabled = true;

                iconPause.Visible = true;
                iconResume.Visible = false;

                Invalidate();
            }
        }

        private void IconRestart_Click(object sender, EventArgs e)
        {
            btnRestart_Click(sender, e); // reuse existing restart logic
        }

        private void ChkMirrorEffect_Checked(object sender, EventArgs e)
        {
            mirrorEffectEnabled = chkMirrorEffect.Checked;
        }

        #region Override Methods

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            int gridOffsetX = (ClientSize.Width - GridWidth * BlockSize) / 2;
            int gridWidthPixels = GridWidth * BlockSize;
            int gridHeightPixels = GridHeight * BlockSize;

            // NEW: Offset grid vertically to reduce gap with bottom panel
            int gridOffsetY = (ClientSize.Height - topPanel.Height - bottomPanel.Height - gridHeightPixels) / 2 + topPanel.Height;

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
                            gridOffsetY + y * BlockSize,
                            BlockSize, BlockSize);
                    }
                }
            }

            // Draw ghost piece
            if (currentShape != null && showGhostPiece)
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
                                gridOffsetY + (ghost.Y + y) * BlockSize,
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
                                gridOffsetY + (currentShape.Y + y) * BlockSize,
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
                Rectangle rect = new Rectangle(gridOffsetX, gridOffsetY, gridWidthPixels, gridHeightPixels);
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
                path.CloseFigure();
                g.DrawPath(roundedBorder, path);
            }

            // Draw score labels
            lblScore.Text = $"Score: {score}";
            lblHighScore.Text = $"High Score: {highScore}";

            // Draw next piece preview
            if (nextShape != null)
            {
                // Position inside grid: slightly left and lower
                int previewX = gridOffsetX + GridWidth * BlockSize - (nextShape.Matrix.GetLength(1) * BlockSize) - 10;
                int previewY = 90;

                // Draw label
                g.DrawString("Next:", new Font("Arial", 12), Brushes.Black, new PointF(previewX - 60, previewY + 10));

                // Draw shape
                for (int y = 0; y < nextShape.Matrix.GetLength(0); y++)
                {
                    for (int x = 0; x < nextShape.Matrix.GetLength(1); x++)
                    {
                        if (nextShape.Matrix[y, x] == 1)
                        {
                            g.FillRectangle(new SolidBrush(nextShape.Color),
                                previewX + x * BlockSize,
                                previewY + y * BlockSize,
                                BlockSize, BlockSize);
                        }
                    }
                }
            }

            // Updated pause overlay: only covers grid
            if (isPaused)
            {
                using (Brush overlay = new SolidBrush(Color.FromArgb(180, Color.Black)))
                {
                    g.FillRectangle(overlay, gridOffsetX, gridOffsetY, gridWidthPixels, gridHeightPixels);
                }

                string pauseText = "PAUSED";
                Font pauseFont = new Font("Arial", 32, FontStyle.Bold);
                SizeF textSize = g.MeasureString(pauseText, pauseFont);
                g.DrawString(pauseText, pauseFont, Brushes.White,
                    (ClientSize.Width - textSize.Width) / 2,
                    gridOffsetY + (gridHeightPixels - textSize.Height) / 2);
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
                    if (mirrorEffectEnabled)
                    {
                        MirrorShape(currentShape);
                        if (IsCollision(currentShape, 0, 0))
                        {
                            // Undo mirror if it causes collision
                            MirrorShape(currentShape);
                        }
                    }
                    break;
                case Keys.P:
                case Keys.Space:
                case Keys.Escape:
                    isPaused = !isPaused;
                    if (isPaused)
                    {
                        gameTimer.Stop();
                        lblScore.Text = $"Score: Paused";
                        lblHighScore.Text = $"High Score: {highScore}";
                        btnRestart.Enabled = false; // Disable restart while paused
                        iconPause.Visible = false;
                        iconResume.Visible = true;
                    }
                    else
                    {
                        gameTimer.Start();
                        lblScore.Text = $"Score: {score}";
                        lblHighScore.Text = $"High Score: {highScore}";
                        btnRestart.Enabled = true; // Re-enable restart
                        iconPause.Visible = true;
                        iconResume.Visible = false;
                    }
                    break;
            }

            Invalidate();
            return true;
        }

        #endregion
    }
}