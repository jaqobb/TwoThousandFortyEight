using System;
using System.Threading;
using TwoThousandFortyEight.Logic;
using TwoThousandFortyEight.Util;

namespace TwoThousandFortyEight
{
    internal static class Bootstrap
    {
        private const int MinimumBoardWidth = 4;
        private const int MaximumBoardWidth = 8;
        private const int MinimumBoardHeight = 4;
        private const int MaximumBoardHeight = 8;

        private static void Main()
        {
            Console.CursorVisible = false;
            Console.WriteLine("Welcome to 2048!");
            Console.WriteLine("Let's setup a few basic settings.");
            Console.WriteLine();
            int boardWidth;
            int boardHeight;
            bool infiniteModeEnabled;
            Console.Write($"Specify the width of the board ({MinimumBoardWidth}..{MaximumBoardWidth}): ");
            while (!int.TryParse(Console.ReadLine(), out boardWidth) || boardWidth is < MinimumBoardWidth or > MaximumBoardWidth)
            {
                Console.Write($"You provided a wrong width, please specify the width of the board ({MinimumBoardWidth}..{MaximumBoardWidth}): ");
            }
            Console.Write($"Specify the height of the board ({MinimumBoardHeight}..{MaximumBoardHeight}): ");
            while (!int.TryParse(Console.ReadLine(), out boardHeight) || boardHeight is < MinimumBoardHeight or > MaximumBoardHeight)
            {
                Console.Write($"You provided a wrong height, please specify the height of the board ({MinimumBoardHeight}..{MaximumBoardHeight}): ");
            }
            Console.Write("Specify if you want your game to be in the infinite mode. In infinite mode, the game will not end once you get the 2048 tile (true | false): ");
            while (!bool.TryParse(Console.ReadLine(), out infiniteModeEnabled))
            {
                Console.Write("You provided a wrong value for the infinite mode state, please specify if you want your game to be in the infinite mode (true | false): ");
            }
            Console.Clear();
            // The best loading screen there can be.
            // And yes, it doesn't actually do anything in the background, it just makes the user wait.
            // But why you might ask? No particular reason.
            var dots = 1;
            var dotsAmount = 3;
            var iteration = 0;
            var iterations = Utils.Random.Next(1, 4);
            while (iteration < iterations)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write("Setting up the game");
                for (var dot = 0; dot < dots; dot++)
                {
                    Console.Write(".");
                }
                Console.SetCursorPosition(0, 0);
                Thread.Sleep(1_000);
                dots++;
                if (dots > dotsAmount)
                {
                    dots = 1;
                    iteration++;
                    Console.SetCursorPosition("Setting up the game".Length, 0);
                    Console.Write("   ");
                }
            }
            Console.Clear();
            var game = new Game(boardWidth, boardHeight, infiniteModeEnabled);
            game.Start();
            while (!game.End())
            {
                game.Tick();
            }
            Console.Clear();
            Console.Write("That's it! ");
            Console.WriteLine(game.HadFinalBlock ? "You won!" : "You lost.");
            Console.WriteLine();
            Console.WriteLine($"Points: {game.Points}");
            Console.WriteLine($"Moves: {game.Moves}");
            Console.WriteLine();
            Console.WriteLine("Press any key to close the game...");
            Console.ReadKey();
        }
    }
}
