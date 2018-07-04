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
using DrawableGrid.Components;
using DrawableGrid.Managers;
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
        private readonly PreviewLine _previewLine;
        private Point _drawEnterPoint;
        private readonly SnappableLineManager _lineManager = new SnappableLineManager();

        public DrawableGridControl()
        {
            InitializeComponent();
            _previewLine = new PreviewLine(GRID_SIZE);
            MainGrid.Children.Add(_previewLine);
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _isDrawing = true;
            _previewLine.Show();
            _drawEnterPoint = e.GetPosition(this);
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var drawReleasePoint = e.GetPosition(this);

            CreateLineInMainGrid(_drawEnterPoint, drawReleasePoint);

            _isDrawing = false;
            _previewLine.Hide();
        }

        private void CreateLineInMainGrid(Point beginning, Point end)
        {
            var line = _lineManager.CreateLine(beginning, end, GRID_SIZE);
            MainGrid.Children.Add(line);
        }

        private void MainGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDrawing) return;

            RenderPreviewLine();
        }

        private void RenderPreviewLine()
        {
            var currentMousePosition = Mouse.GetPosition(this);
            _lineManager.MoveLine(_previewLine, _drawEnterPoint, currentMousePosition);
            _previewLine.UpdateLabel();
        }
    }
}
