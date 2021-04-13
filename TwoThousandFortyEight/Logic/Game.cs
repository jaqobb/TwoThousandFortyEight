using System;
using TwoThousandFortyEight.Util;

namespace TwoThousandFortyEight.Logic
{
    public class Game
    {
        public readonly Board Board;
        public int Points
        {
            get;
            private set;
        }
        public int Moves
        {
            get;
            private set;
        }
        public readonly bool InfiniteModeEnabled;
        public bool HadFinalBlock
        {
            get;
            private set;
        }

        public Game(int boardWidth, int boardHeight, bool infiniteModeEnabled)
        {
            Board               = new Board(boardWidth, boardHeight);
            Points              = 0;
            Moves               = 0;
            InfiniteModeEnabled = infiniteModeEnabled;
            HadFinalBlock       = false;
        }

        public bool IsAbleToMove()
        {
            for (int y = 0; y < Board.Height; y++)
            {
                for (int x = 0; x < Board.Width; x++)
                {
                    int value = Board.Tiles[x, y];
                    if (value == 0)
                    {
                        return true;
                    }
                    foreach (Direction direction in Directions.Values)
                    {
                        if (direction == Directions.Up && y == 0 || direction == Directions.Down && y == Board.Height - 1 || direction == Directions.Left && x == 0 || direction == Directions.Right && x == Board.Width - 1)
                        {
                            continue;
                        }
                        int directionedValue = Board.Tiles[x + direction.Width, y + direction.Height];
                        if (directionedValue == 0 || value / directionedValue == 1)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void MakeMove(Direction direction)
        {
            bool boardChanged = false;
            if (direction == Directions.Up)
            {
                Movement:
                for (int y = 0; y < Board.Height; y++)
                {
                    for (int x = 0; x < Board.Width; x++)
                    {
                        int value = Board.Tiles[x, y];
                        if (value == 0)
                        {
                            continue;
                        }
                        if (y == 0)
                        {
                            continue;
                        }
                        if (MakeMove0(x, y, value, direction))
                        {
                            boardChanged = true;
                            goto Movement;
                        }
                    }
                }
            }
            if (direction == Directions.Down)
            {
                Movement:
                for (int y = Board.Height - 1; y >= 0; y--)
                {
                    for (int x = Board.Width - 1; x >= 0; x--)
                    {
                        int value = Board.Tiles[x, y];
                        if (value == 0)
                        {
                            continue;
                        }
                        if (y == Board.Height - 1)
                        {
                            continue;
                        }
                        if (MakeMove0(x, y, value, direction))
                        {
                            boardChanged = true;
                            goto Movement;
                        }
                    }
                }
            }
            if (direction == Directions.Left)
            {
                Movement:
                for (int y = 0; y < Board.Height; y++)
                {
                    for (int x = 0; x < Board.Width; x++)
                    {
                        int value = Board.Tiles[x, y];
                        if (value == 0)
                        {
                            continue;
                        }
                        if (x == 0)
                        {
                            continue;
                        }
                        if (MakeMove0(x, y, value, direction))
                        {
                            boardChanged = true;
                            goto Movement;
                        }
                    }
                }
            }
            if (direction == Directions.Right)
            {
                Movement:
                for (int y = Board.Height - 1; y >= 0; y--)
                {
                    for (int x = Board.Width - 1; x >= 0; x--)
                    {
                        int value = Board.Tiles[x, y];
                        if (value == 0)
                        {
                            continue;
                        }
                        if (x == Board.Width - 1)
                        {
                            continue;
                        }
                        if (MakeMove0(x, y, value, direction))
                        {
                            boardChanged = true;
                            goto Movement;
                        }
                    }
                }
            }
            if (boardChanged)
            {
                Moves++;
                Board.AddRandomBlock();
            }
        }

        private bool MakeMove0(int x, int y, int value, Direction direction)
        {
            int directionedValue = Board.Tiles[x + direction.Width, y + direction.Height];
            if (directionedValue != 0 && value / directionedValue != 1)
            {
                return false;
            }
            Board.Tiles[x, y]                                      = 0;
            Board.Tiles[x + direction.Width, y + direction.Height] = value + directionedValue;
            if (directionedValue != 0)
            {
                Points += value + directionedValue;
                if (value + directionedValue == 2_048)
                {
                    HadFinalBlock = true;
                }
            }
            return true;
        }

        public bool End()
        {
            return HadFinalBlock && !InfiniteModeEnabled || !IsAbleToMove();
        }

        public void Start()
        {
            Board.AddDefaultBlock();
            if (Utils.Random.Next(0, 2) == 0)
            {
                Board.AddRandomBlock();
            }
        }

        public void Tick()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Points: {Points}");
            Console.WriteLine($"Moves: {Moves}");
            Console.WriteLine();
            Board.Render();
            Direction? direction = Directions.Parse(Console.ReadKey().KeyChar);
            if (direction != null)
            {
                MakeMove(direction);
            }
        }
    }
}
