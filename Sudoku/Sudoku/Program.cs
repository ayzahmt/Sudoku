using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int[,] sudoku = new int[9, 9];

            sudoku = new int[,] {
                { 1, 0, 0, 0, 0, 0, 3, 0, 0 },
                { 0, 9, 0, 0, 7, 0, 0, 2, 0 },
                { 2, 0, 0, 0, 0, 9, 5, 0, 0 },
                { 3, 0, 0, 0, 0, 0, 4, 0, 0 },
                { 0, 5, 0, 0, 8, 0, 0, 6, 0 },
                { 0, 0, 1, 0, 0, 0, 0, 0, 9 },
                { 0, 0, 4, 6, 0, 0, 0, 0, 7 },
                { 0, 6, 0, 0, 5, 0, 0, 1, 0 },
                { 0, 0, 9, 0, 0, 0, 0, 0, 3 }
            };

            var ins = new Program();
            if (ins.SolveSudoku(sudoku))
            {
                #region Print Grid

                Console.WriteLine("Solution is:\n");

                for (int i = 0; i < sudoku.GetLength(0); i++)
                {
                    for (int j = 0; j < sudoku.GetLength(1); j++)
                    {
                        if (j % 3 == 0) Console.Write("| ");
                        Console.Write(sudoku[i, j] + " ");
                    }
                    Console.Write("|\n");
                    if ((i + 1) % 3 == 0 && i < 8)
                    {
                        for (int k = 0; k < sudoku.GetLength(0); k++)
                        {
                            if (k % 3 == 0) Console.Write("| ");
                            Console.Write("- ");
                        }
                        Console.Write("|\n");
                    }
                }
                Console.Read();
                #endregion
            }
            else
            {
                Console.WriteLine("There is no solution!");
            }
        }

        bool SolveSudoku(int[,] sudoku)
        {
            if (IsGridCompleted(sudoku)) return true;

            for (int i = 0; i < sudoku.GetLength(0); i++)
            {
                for (int j = 0; j < sudoku.GetLength(1); j++)
                {
                    if (sudoku[i, j] == 0)
                    {
                        for (int k = 1; k <= 9; k++)
                        {
                            if (IsFieldAvailable(sudoku, i, j, k))
                            {
                                sudoku[i, j] = k;

                                if (SolveSudoku(sudoku))
                                {
                                    return true;
                                }

                                sudoku[i, j] = 0;
                            }
                        }
                        return false;
                    }
                }
            }

            return false;

        }

        bool IsGridCompleted(int[,] sudoku)
        {
            for (int i = 0; i < sudoku.GetLength(0); i++)
            {
                for (int j = 0; j < sudoku.GetLength(1); j++)
                {
                    if (sudoku[i, j] == 0) return false;
                }
            }
            return true;
        }

        bool IsFieldAvailable(int[,] sudoku, int row, int col, int number)
        {
            #region ControlRow
            for (int i = 0; i < sudoku.GetLength(0); i++)
            {
                if (sudoku[row, i] == number) return false;
            }
            #endregion

            #region ControlColumn
            for (int i = 0; i < sudoku.GetLength(0); i++)
            {
                if (sudoku[i, col] == number) return false;
            }
            #endregion

            #region ControlSubGrid
            for (int i = 0; i < sudoku.GetLength(0) / 3; i++)
            {
                for (int j = 0; j < sudoku.GetLength(1) / 3; j++)
                {
                    if (sudoku[(i + (row - row % 3)), (j + (col - col % 3))] == number) return false;
                }
            }
            #endregion

            return true;
        }
    }
}
