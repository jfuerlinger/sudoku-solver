using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SudokuSolver.Core.Model
{
    public class BoundingBox
    {
        public Point TopLeft { get; set; }
        public Point TopRight { get; set; }
        public Point BottomRight { get; set; }
        public Point BottumLeft { get; set; }
    }
}
