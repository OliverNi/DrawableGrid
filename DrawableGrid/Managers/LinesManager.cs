using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DrawableGrid.Components;
using DrawableGrid.Events;
using DrawableGrid.Utilities;

namespace DrawableGrid.Managers
{
    public class LinesManager
    {
        private readonly SnappableLineManager _lineManager = new SnappableLineManager();
        private readonly PreviewLineManager _previewLineManager;
        private readonly int _gridSize;
        private readonly Grid _grid;

        public LinesManager(Grid grid, int gridSize)
        {
            _grid = grid;
            _gridSize = gridSize;
            _previewLineManager = new PreviewLineManager(grid, gridSize);
            SubscribeToEventsFromGrid(grid);
        }

        public SnappableLine CreateSnappableLineInGrid(Point start, Point end)
        {
            var line = _lineManager.CreateLine(start, end, _gridSize);
            _grid.Children.Add(line);

            line.MouseDown += _previewLineManager.OnLineTargeted;
            line.LineDragged += _previewLineManager.OnEditModeActivated;
            line.LineDragged += OnLineDrag;
            line.LineDraggedReleased += OnLineDragged;
            return line;
        }

        private void OnLineDragged(DrawableLine source, MouseButtonEventArgs e)
        {
            if (source is SnappableLine line)
                _lineManager.MoveLine(line, LineUtilities.StartPoint(line.Line), e.GetPosition(_grid));
            else
                source.Move(LineUtilities.StartPoint(source.Line), e.GetPosition(_grid));
        }

        public void ActivatePreviewLineFrom(Point point)
        {
            _previewLineManager.ActivatePreviewLineFrom(point);
        }

        public void DeactivatePreviewLine()
        {
            _previewLineManager.DeactivatePreviewLine();
        }

        public void OnMouseMove(object source, MouseEventArgs e)
        {
            if (_previewLineManager.IsActive() && source is IInputElement element)
                RenderPreviewLine(LineUtilities.StartPoint(_previewLineManager.PreviewLine.Line),
                    element);
        }

        private void OnLineDrag(DrawableLine source, LineDragEventArgs e)
        {
            _grid.MouseMove += e.ResizeGrip.OnMouseMove;
            e.ResizeGrip.MouseUp += _previewLineManager.OnEditModeDeactivated;
        }

        private void RenderPreviewLine(Point enterPoint, IInputElement source)
        {
            var currentMousePosition = Mouse.GetPosition(source);
            _lineManager.MoveLine(_previewLineManager.PreviewLine, enterPoint, currentMousePosition);
            _previewLineManager.Update();
        }

        private void SubscribeToEventsFromGrid(Grid grid)
        {
            grid.MouseMove += OnMouseMove;
        }
    }
}