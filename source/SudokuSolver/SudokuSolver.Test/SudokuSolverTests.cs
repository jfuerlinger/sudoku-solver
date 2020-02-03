using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuSolver.Core;
using SudokuSolver.Core.Model;
using System.Drawing;

namespace SudokuSolver.Test
{
    [TestClass]
    public class SudokuSolverTests
    {
        private const int ROWS = 9;
        private const int COLUMNS = 9;

        [TestMethod]
        public void CheckHorizontal_CallWithInvalidNumber_ShouldReturnFalse()
        {
            // Arrange
            int[,] sudoku = new int[ROWS, COLUMNS];

            int row = 1;
            sudoku[row, 0] = 7;
            sudoku[row, 6] = 3;

            // Act
            bool isValid = RecursiveSudokuSolver.CheckHorizontal(sudoku, new SudokuCoordinate(row, 5), 3);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void CheckHorizontal_CallWithValidNumber_ShouldReturnTrue()
        {
            // Arrange
            int[,] sudoku = new int[ROWS, COLUMNS];

            int row = 1;
            sudoku[row, 0] = 7;
            sudoku[row, 6] = 3;

            // Act
            bool isValid = RecursiveSudokuSolver.CheckHorizontal(sudoku, new SudokuCoordinate(row, 5), 4);

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void CheckVertical_CallWithInvalidNumber_ShouldReturnFalse()
        {
            // Arrange
            int[,] sudoku = new int[ROWS, COLUMNS];

            int column = 1;
            sudoku[0, column] = 7;
            sudoku[5, column] = 3;

            // Act
            bool isValid = RecursiveSudokuSolver.CheckVertical(sudoku, new SudokuCoordinate(4, column), 3);

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void CheckVertical_CallWithValidNumber_ShouldReturnTrue()
        {
            // Arrange
            int[,] sudoku = new int[ROWS, COLUMNS];

            int column = 1;
            sudoku[0, column] = 7;
            sudoku[5, column] = 3;

            // Act
            bool isValid = RecursiveSudokuSolver.CheckVertical(sudoku, new SudokuCoordinate(4, column), 4);

            // Assert
            Assert.IsTrue(isValid);
        }
    }
}
