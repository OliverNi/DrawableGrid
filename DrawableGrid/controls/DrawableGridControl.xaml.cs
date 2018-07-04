using System;
using System.Collections.Generic;
using System.Drawing;
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
using Brushes = System.Windows.Media.Brushes;
using Point = System.Windows.Point;

namespace DrawableGrid.controls
{
    /// <summary>
    /// Interaction logic for DrawableGridControl.xaml
    /// </summary>
    public partial class DrawableGridControl : UserControl
    {
        private static readonly int GRID_SIZE = 50;
        private bool _isDrawing = false;
        private readonly Line _previewLine;
        private Point _drawEnterPoint;

        public DrawableGridControl()
        {
            InitializeComponent();
            _previewLine = new Line
            {
                Stroke = Brushes.Gray,
                StrokeThickness = 2
            };
            MainGrid.Children.Add(_previewLine);
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _isDrawing = true;
            _drawEnterPoint = SnappedPointOf(e.GetPosition(this));
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var drawReleasePoint = e.GetPosition(this);
            CreateLineInMainGrid(_drawEnterPoint, drawReleasePoint);
            _isDrawing = false;
            _previewLine.Visibility = Visibility.Hidden;
        }

        private int CreateLineInMainGrid(Point beginning, Point end)
        {
            var line = new Line
            {
                Stroke = Brushes.Black,
                X1 = beginning.X,
                Y1 = beginning.Y,
                X2 = end.X,
                Y2 = end.Y,
                StrokeThickness = 2
            };
            SnapToGrid(line);
            return MainGrid.Children.Add(line);
        }

        private void MainGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDrawing) return;

            RenderPreviewLine();
            _previewLine.Visibility = Visibility.Visible;
        }

        private void RenderPreviewLine()
        {
            var currentMousePosition = SnappedPointOf(Mouse.GetPosition(this));
            _previewLine.X1 = _drawEnterPoint.X;
            _previewLine.Y1 = _drawEnterPoint.Y;
            _previewLine.X2 = currentMousePosition.X;
            _previewLine.Y2 = currentMousePosition.Y;
            SnapToGrid(_previewLine);
        }

        private void SnapToGrid(Line line)
        {
            var snappedPoint = SnappedPointOf(new Point(line.X2, line.Y2));
            line.X2 = snappedPoint.X;
            line.Y2 = snappedPoint.Y;
        }

        private Point SnappedPointOf(Point point)
        {
            var xSnap = point.X % GRID_SIZE;
            var ySnap = point.Y % GRID_SIZE;

            // If it's less than half the grid size, snap left/up 
            // (by subtracting the remainder), 
            // otherwise move it the remaining distance of the grid size right/down
            // (by adding the remaining distance to the next grid point).
            if (xSnap <= GRID_SIZE / 2.0)
                xSnap *= -1;
            else
                xSnap = GRID_SIZE - xSnap;
            if (ySnap <= GRID_SIZE / 2.0)
                ySnap *= -1;
            else
                ySnap = GRID_SIZE - ySnap;

            xSnap += point.X;
            ySnap += point.Y;
            return new Point(xSnap, ySnap);
        }
    }
}
