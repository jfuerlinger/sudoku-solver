using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Linq;
using SudokuSolver.Core;

namespace SudokuSolver.ConsoleApp
{

    static class Program
    {
        private const string INPUT_FILE = @"c:\temp\sudoku.jpeg";

        public static void Main()
        {
            // Get the path and filename to process from the user.
            Console.WriteLine("Optical Character Recognition:");
            //Console.Write("Enter the path to an image with text you wish to read: ");
            string imageFilePath = INPUT_FILE;

            if (File.Exists(imageFilePath))
            {
                // Call the REST API method.
                Console.WriteLine("\nWait a moment for the results to appear.\n");
                var result = OcrProcessor.ProcessImage(imageFilePath).Result;

                foreach (var entry in result)
                {
                    Console.WriteLine($"{entry.Text,2} => TopLeft[{entry.BoundingBox.TopLeft.X},{entry.BoundingBox.TopLeft.Y}] TopRight[{entry.BoundingBox.TopRight.X},{entry.BoundingBox.TopRight.Y}] BottumRight[{entry.BoundingBox.BottumLeft.X},{entry.BoundingBox.BottomRight.Y}] BottomLeft[{entry.BoundingBox.BottumLeft.X},{entry.BoundingBox.BottumLeft.Y}]");
                }
            }
            else
            {
                Console.WriteLine("\nInvalid file path");
            }
            Console.WriteLine("\nPress Enter to exit...");
            Console.ReadLine();
        }

    }
}