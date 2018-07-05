using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Shapes;
using DrawableGrid.Utilities;

namespace DrawableGrid.Components
{
    public class LineResizeGrip : ResizeGrip
    {
        public enum DragDirection
        {
            Start,
            End
        }

        public DragDirection Direction { get; }

        public LineResizeGrip(DragDirection direction)
        {
            Direction = direction;
        }

        public void OnMouseMove(object source, MouseEventArgs e)
        {
            Canvas.SetLeft(this, e.GetPosition((IInputElement)source).X);
            Canvas.SetTop(this, e.GetPosition((IInputElement)source).Y);
        }

        public void MoveToEndOf(Line line)
        {
            var lineEnd = LineUtilities.EndPoint(line);
            Canvas.SetLeft(this, lineEnd.X - 10);
            Canvas.SetTop(this, lineEnd.Y - 10);
        }
    }
}