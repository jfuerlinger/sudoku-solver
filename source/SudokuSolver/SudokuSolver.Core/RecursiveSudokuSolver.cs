using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Drawing;
using SudokuSolver.Core.Model;

namespace SudokuSolver.Core
{
    public class RecursiveSudokuSolver : ISodukuSolver
    {
        public int[,] Solve(int[,] sudoku)
        {
            throw new NotImplementedException();
        }

        private int[,] CopyArray(int[,] source)
        {
            int[,] dest = new int[source.GetLength(0), source.GetLength(1)];

            for (int i = 0; i < source.GetLength(0); i++)
            {
                for (int j = 0; j < source.GetLength(1); j++)
                {
                    dest[i, j] = source[i, j];
                }
            }

            return dest;
        }

        public static bool CheckQuadrant(int[,] sudoku, SudokuCoordinate coordinate, int value)
        {
            int minRow, maxRow;
            int minColumn, maxColumn;

            throw new NotImplementedException();
        }

        public static bool CheckHorizontal(int[,] sudoku, SudokuCoordinate coordinates, int value)
        {
            for (int i = 0; i < sudoku.GetLength(1); i++)
            {
                if(sudoku[coordinates.Row, i] == value)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool CheckVertical(int[,] sudoku, SudokuCoordinate coordinates, int value)
        {
            for (int i = 0; i < sudoku.GetLength(0); i++)
            {
                if (sudoku[i, coordinates.Column] == value)
                {
                    return false;
                }
            }

            return true;
        }

    }
}
