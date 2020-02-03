using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver.Core.Model
{
    public class CellEntry
    {


        public enum CellEntryType
        {
            Empty,
            PreDefined,
            Calculated
        }

        public int? Value { get; set; }
    }
}
