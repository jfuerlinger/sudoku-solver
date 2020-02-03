using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver.Core.Model
{
    public class SudokuCoordinate
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public SudokuCoordinate(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
