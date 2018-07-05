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
using DrawableGrid.Events;
using DrawableGrid.Managers;
using DrawableGrid.Utilities;
using Brushes = System.Windows.Media.Brushes;
using Point = System.Windows.Point;

namespace DrawableGrid.controls
{
    /// <summary>
    /// Interaction logic for DrawableGridControl.xaml
    /// </summary>
    public partial class DrawableGridControl : UserControl
    {
        private const int GridSize = 20;
        private bool _isDrawing;
        private bool _isEditing;
        private Point _drawEnterPoint;
        private readonly LinesManager _linesManager;

        public DrawableGridControl()
        {
            InitializeComponent();
            _linesManager = new LinesManager(MainGrid, GridSize);
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
           if (_isDrawing || _isEditing) return;
            _isDrawing = true;
            _drawEnterPoint = e.GetPosition(this);
            _linesManager.ActivatePreviewLineFrom(_drawEnterPoint);
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!_isDrawing) return;

            var drawReleasePoint = e.GetPosition(this);
            var line = _linesManager.CreateSnappableLineInGrid(_drawEnterPoint, drawReleasePoint);
            line.LineDragged += OnEditLine;
            _isDrawing = false;
            _linesManager.DeactivatePreviewLine();
        }

        private void OnEditLine(object line, LineDragEventArgs e)
        {
            _isEditing = true;
            e.ResizeGrip.MouseLeftButtonUp += OnEditLineReleased;
        }

        private void OnEditLineReleased(object line, MouseButtonEventArgs e)
        {
            _isEditing = false;
        }
    }
}
