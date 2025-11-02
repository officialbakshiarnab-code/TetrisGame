using System.Drawing;

namespace TetrisGame
{
    public class Shape
    {
        public int[,] Matrix;
        public int X = 3, Y = 0;
        public Color Color;

        public Shape(int[,] matrix, Color color)
        {
            Matrix = matrix;
            Color = color;
        }

        public void Rotate()
        {
            int rows = Matrix.GetLength(0);
            int cols = Matrix.GetLength(1);
            int[,] rotated = new int[cols, rows];

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    rotated[x, rows - 1 - y] = Matrix[y, x];
                }
            }

            Matrix = rotated;
        }
    }
}