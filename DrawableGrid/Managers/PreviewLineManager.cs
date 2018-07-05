using System;
using System.Windows;
using System.Windows.Controls;
using DrawableGrid.Components;
using DrawableGrid.Events;
using DrawableGrid.Utilities;

namespace DrawableGrid.Managers
{
    public class PreviewLineManager : SnappableLineManager
    {
        public PreviewLine PreviewLine { get; }

        public PreviewLineManager(Grid grid, int gridSize)
        {
            PreviewLine = new PreviewLine(gridSize);
            grid.Children.Add(PreviewLine);
        }

        public void ActivatePreviewLineFrom(Point point)
        {
            PreviewLine.Line.X1 = point.X;
            PreviewLine.Line.Y1 = point.Y;
            PreviewLine.Show();
        }

        public void DeactivatePreviewLine()
        {
            PreviewLine.Hide();
        }

        public void OnLineTargeted(object line, EventArgs e)
        {
            if (line is SnappableLine snappableLine)
                LineUtilities.MoveLineTo(PreviewLine, snappableLine);
        }

        public void OnEditModeActivated(DrawableLine source, LineDragEventArgs e)
        {
            if (e.Direction is LineResizeGrip.DragDirection.Start)
                LineUtilities.ChangeDirection(PreviewLine.Line);
            PreviewLine.Show();
        }

        public void OnEditModeDeactivated(object source, EventArgs e)
        {
            DeactivatePreviewLine();
        }

        public bool IsActive()
        {
            return PreviewLine.Line.IsVisible;
        }

        public void Update()
        {
            PreviewLine.UpdateLabel();
        }
    }
}