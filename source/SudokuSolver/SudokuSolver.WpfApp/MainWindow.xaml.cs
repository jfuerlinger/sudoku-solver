using SudokuSolver.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SudokuSolver.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Initialized(object sender, EventArgs e)
        {
            //Application.Current.MainWindow.WindowState = WindowState.Maximized;

            var result = await OcrProcessor.ProcessImage(@"c:\temp\sudoku.jpeg");

            foreach(var entry in result)
            {
                // Initialize a new Rectangle
                Rectangle r = new Rectangle();

                
                // Set up rectangle's size
                r.Width = Math.Abs(entry.BoundingBox.TopRight.X - entry.BoundingBox.TopLeft.X);
                r.Height = Math.Abs(entry.BoundingBox.BottumLeft.Y - entry.BoundingBox.TopLeft.Y);

                // Set up the Background color
                //r.Fill = Brushes.Red;
                r.Stroke = new SolidColorBrush(Colors.Red);

                // Set up the position in the window, at mouse coordonate
                Canvas.SetTop(r, entry.BoundingBox.TopLeft.Y);
                Canvas.SetLeft(r, entry.BoundingBox.TopLeft.X);



                // Add rectangle to the Canvas
                cvDrawingArea.Children.Add(r);
                
                
                TextBlock textBlock = new TextBlock();
                textBlock.Foreground = new SolidColorBrush(Colors.Red);
                textBlock.Text = entry.Text;
                textBlock.Width = r.Width;
                textBlock.Height = r.Height;
                textBlock.FontSize = 32;

                Canvas.SetTop(textBlock, entry.BoundingBox.TopLeft.Y);
                Canvas.SetLeft(textBlock, entry.BoundingBox.TopLeft.X);

                cvDrawingArea.Children.Add(textBlock);
            }
        }
    }
}
