using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver.Core.Model
{
    public class RecognitionEntry
    {
        public BoundingBox BoundingBox { get; set; }
        public string Text { get; set; }
    }
}
