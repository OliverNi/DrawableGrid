using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;

namespace DrawableGrid.Components
{
    public class DrawableLine : Canvas
    {
        public Line Line { get; }

        public DrawableLine(Point start, Point end)
            : this(start, end, Brushes.Black) { }

        public DrawableLine(Point start, Point end, Brush brush)
        {
            Line = new Line
            {
                Stroke = brush,
                StrokeThickness = 2,
                X1 = start.X,
                Y1 = start.Y,
                X2 = end.X,
                Y2 = end.Y
            };
            this.Children.Add(Line);
        }

        public virtual void Move(Point start, Point end)
        {
            Line.X1 = start.X;
            Line.Y1 = start.Y;
            Line.X2 = end.X;
            Line.Y2 = end.Y;
        }
    }
}