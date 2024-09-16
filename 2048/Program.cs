using System;

namespace _2048
{
    class Program
    {        
        static void Main(string[] args)
        {
            int[][] matrix = new int[4][] { new int[4], new int[4], new int[4], new int[4] };
            int totalScore = 0;

            Helper.GenerateNewMatrix(matrix);
            Helper.GenerateNewMatrix(matrix);
            Console.WriteLine($"\n**********\nSCORE: {totalScore}");
            Helper.PrintMatrix(matrix);
            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();

            while (keyInfo.Key != ConsoleKey.Enter && Helper.HasLegalMoves(matrix))
            {
                var oldMatrix = Helper.CopyMatrix(matrix);

                keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        totalScore += Helper.UpKeyPressed(matrix);
                        break;
                    case ConsoleKey.DownArrow:
                        totalScore += Helper.DownKeyPressed(matrix);
                        break;
                    case ConsoleKey.LeftArrow:
                        totalScore += Helper.LeftKeyPressed(matrix);
                        break;
                    case ConsoleKey.RightArrow:
                        totalScore += Helper.RightKeyPressed(matrix);
                        break;
                    default:
                        break;
                }

                if (!Helper.MatricesAreEqual(oldMatrix, matrix))
                {
                    Console.Clear();
                    Console.WriteLine($"\n\n**********\nSCORE: {totalScore}");
                    Helper.GenerateNewMatrix(matrix);
                    Helper.PrintMatrix(matrix);
                }
            }

            Console.WriteLine("\n\n\n******************* GAME OVER *******************\n\n\n");
            Console.WriteLine($"\n\n**********\nSCORE: {totalScore}\n**********");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
