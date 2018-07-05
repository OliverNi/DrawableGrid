using System;
using DrawableGrid.Components;

namespace DrawableGrid.Events
{
    public class LineDragEventArgs : EventArgs
    {
        public LineResizeGrip.DragDirection Direction { get; }
        public LineResizeGrip ResizeGrip { get; }

        public LineDragEventArgs(LineResizeGrip resizeGrip, LineResizeGrip.DragDirection direction)
        {
            ResizeGrip = resizeGrip;
            Direction = direction;
        }
    }
}