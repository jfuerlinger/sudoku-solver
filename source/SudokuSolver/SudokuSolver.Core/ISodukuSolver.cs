using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver.Core
{
    public interface ISodukuSolver
    {
        int[,] Solve(int[,] sudoku);
    }
}
