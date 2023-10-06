using System;
using TwoThousandFortyEight.Util;

namespace TwoThousandFortyEight.Logic
{
    public class Board
    {
        public readonly int Width;
        public readonly int Height;
        public readonly int[,] Tiles;

        public Board(int width, int height)
        {
            Width = width;
            Height = height;
            Tiles = new int[width, height];
        }

        public void AddDefaultBlock()
        {
            Tiles[0, Height - 1] = 2;
        }

        public void AddRandomBlock()
        {
            for (var iteration = 0; iteration < Width * Height; iteration++)
            {
                var x = Utils.Random.Next(0, Width);
                var y = Utils.Random.Next(0, Height);
                if (Tiles[x, y] != 0)
                {
                    continue;
                }
                if (Utils.Random.Next(0, 4) == 0)
                {
                    Tiles[x, y] = 4;
                }
                else
                {
                    Tiles[x, y] = 2;
                }
                break;
            }
        }

        public void Render()
        {
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    Console.Write($"{Tiles[x, y],6}");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
