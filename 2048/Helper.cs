using System;
using System.Collections.Generic;

namespace _2048
{
    public class Helper
    {
        private static Random random = new Random();
        private static Dictionary<string, bool> HasMoves = new Dictionary<string, bool>()
        {
            {"left", true}, {"right", true}, {"up", true}, {"down", true}
        };

        private static int GenerateNewNumber()
        {
            double probabilityOfTwo = 0.7;         
            double randomNumber = random.NextDouble();

            if (randomNumber < probabilityOfTwo)
            {
                return 2;
            }
            else
            {
                return 4;
            }
        }

        public static int[][] GenerateNewMatrix(int[][] oldMatrix)
        {
            bool hasSpace = false;

            foreach (int[] matrix in oldMatrix)
            {
                foreach(int value in matrix)
                {
                    if (value == 0)
                    {
                        hasSpace = true;
                        break;
                    }
                }

                if (hasSpace)
                    break;
            }

            if (!hasSpace)
            {
                Console.WriteLine("\n\n\n******************* GAME OVER *******************\n\n\n");
                Console.ReadKey();
                Environment.Exit(0);
            }

            int[][] newMatrix = new int[4][] { new int[4], new int[4], new int[4], new int[4] };

            var rnd2 = random.Next(0, 16);
            if (oldMatrix[rnd2 / 4][rnd2 % 4] != 0)
            {
                GenerateNewMatrix(oldMatrix);
            }
            else
            {
                newMatrix = oldMatrix;
                newMatrix[rnd2 / 4][rnd2 % 4] = GenerateNewNumber();
            }

            return newMatrix;
        }

        public static bool HasLegalMoves(int[][] receivedMatrix) 
        {
            var fixedMatrix = Helper.CopyMatrix(receivedMatrix);

            var changeMatrix = Helper.CopyMatrix(receivedMatrix);
            LeftKeyPressed(changeMatrix);
            if (!Helper.MatricesAreEqual(fixedMatrix, changeMatrix))
            {
                return true;
            }

            changeMatrix = Helper.CopyMatrix(receivedMatrix);
            RightKeyPressed(changeMatrix);
            if (!Helper.MatricesAreEqual(fixedMatrix, changeMatrix))
            {
                return true;
            }

            changeMatrix = Helper.CopyMatrix(receivedMatrix);
            UpKeyPressed(changeMatrix);
            if (!Helper.MatricesAreEqual(fixedMatrix, changeMatrix))
            {
                return true;
            }

            changeMatrix = Helper.CopyMatrix(receivedMatrix);
            DownKeyPressed(changeMatrix);
            if (!Helper.MatricesAreEqual(fixedMatrix, changeMatrix))
            {
                return true;
            }

            return false;
        }

        public static void PrintMatrix(int[][] matrix)
        {
            int maxDigits = 0;

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    int digits = matrix[i][j].ToString().Length;
                    if (digits > maxDigits)
                    {
                        maxDigits = digits;
                    }
                }
            }

            Console.WriteLine();
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    Console.Write(matrix[i][j].ToString().PadLeft(maxDigits + 1));
                }
                Console.WriteLine();
            }
            Console.WriteLine("*****");
        }

        public static int[][] CopyMatrix(int[][] original)
        {
            int[][] copy = new int[4][] { new int[4], new int[4], new int[4], new int[4] };

            for (int i = 0; i < original.Length; i++)
            {
                for(int j = 0; j < original.Length; j++)
                {
                    copy[i][j] = original[i][j];
                }
            }

            return copy;
        }

        public static bool MatricesAreEqual(int[][] matrix1, int[][] matrix2)
        {
            for (int i = 0; i < matrix1.Length; i++)
            {
                for (int j = 0; j < matrix1[i].Length; j++)
                {
                    if (matrix1[i][j] != matrix2[i][j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static void MoveNumbersLeft(int[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                int insertPosition = 0;
                for (int j = 0; j < matrix.Length; j++)
                {
                    if (matrix[i][j] != 0)
                    {
                        matrix[i][insertPosition] = matrix[i][j];

                        if (insertPosition != j)
                        {
                            matrix[i][j] = 0;
                        }
                        insertPosition++;
                    }
                }
            }
        }

        public static int LeftKeyPressed(int[][] matrix)
        {
            int score = 0;

            MoveNumbersLeft(matrix);
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix.Length; j++)
                {
                    if (j+1 < matrix.Length && matrix[i][j] == matrix[i][j+1] && matrix[i][j] != 0)
                    {
                        matrix[i][j] *= 2;
                        score += matrix[i][j];
                        matrix[i][j + 1] = 0;
                    }
                }
            }
            MoveNumbersLeft(matrix);

            return score;
        }

        private static void MoveNumbersRight(int[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                int insertPosition = matrix.Length - 1;
                for (int j = matrix.Length - 1; j >= 0; j--)
                {
                    if (matrix[i][j] != 0)
                    {
                        matrix[i][insertPosition] = matrix[i][j];

                        if (insertPosition != j)
                        {
                            matrix[i][j] = 0;
                        }
                        insertPosition--;
                    }
                }
            }
        }

        public static int RightKeyPressed(int[][] matrix)
        {
            int score = 0;

            MoveNumbersRight(matrix);
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = matrix.Length - 1; j >= 0; j--)
                {
                    if (j > 0 && matrix[i][j] == matrix[i][j - 1] && matrix[i][j] != 0)
                    {
                        matrix[i][j] *= 2;
                        score += matrix[i][j];
                        matrix[i][j - 1] = 0;
                    }
                }
            }
            MoveNumbersRight(matrix);

            return score;
        }

        private static void MoveNumbersDown(int[][] matrix)
        {
            for (int j = 0; j < matrix.Length; j++)
            {
                int insertPosition = matrix.Length - 1;
                for (int i = matrix.Length - 1; i >= 0; i--)
                {
                    if (matrix[i][j] != 0)
                    {
                        matrix[insertPosition][j] = matrix[i][j];

                        if (insertPosition != i)
                        {
                            matrix[i][j] = 0;
                        }
                        insertPosition--;   
                    }
                }
            }
        }

        public static int DownKeyPressed(int[][] matrix)
        {
            int score = 0;

            MoveNumbersDown(matrix);
            for (int j = 0; j < matrix.Length; j++)
            {
                for (int i = matrix.Length - 1; i >= 0; i--)
                {
                    if (i > 0 && matrix[i][j] == matrix[i - 1][j] && matrix[i][j] != 0)
                    {
                        matrix[i][j] *= 2;
                        score += matrix[i][j];
                        matrix[i - 1][j] = 0;
                    }
                }
            }
            MoveNumbersDown(matrix);

            return score;
        }

        private static void MoveNumbersUp(int[][] matrix)
        {
            for (int j = 0; j < matrix.Length; j++)
            {
                int insertPosition = 0;
                for (int i = 0; i < matrix.Length; i++)
                {
                    if (matrix[i][j] != 0)
                    {
                        matrix[insertPosition][j] = matrix[i][j];

                        if (insertPosition != i)
                        {
                            matrix[i][j] = 0;
                        }
                        insertPosition++;
                    }
                }
            }
        }

        public static int UpKeyPressed(int[][] matrix)
        {
            int score = 0;

            MoveNumbersUp(matrix);
            for (int j = 0; j < matrix.Length; j++)
            {
                for (int i = 0; i < matrix.Length - 1; i++)
                {
                    if (matrix[i][j] == matrix[i + 1][j] && matrix[i][j] != 0)
                    {
                        matrix[i][j] *= 2;
                        score += matrix[i][j];
                        matrix[i + 1][j] = 0;
                    }
                }
            }
            MoveNumbersUp(matrix);

            return score;
        }
    }
}
