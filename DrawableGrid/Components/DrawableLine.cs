using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using DrawableGrid.Utilities;
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;

namespace DrawableGrid.Components
{
    public class DrawableLine : Canvas
    {
        public Line Line { get; }
        protected readonly LengthLabel Label;

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
         
            Label = new LengthLabel(Line);
            PositionLabelBelowCenterOfLine();
            this.Children.Add(Label);
            this.Children.Add(Line);
        }

        public virtual void Move(Point start, Point end)
        {
            Line.X1 = start.X;
            Line.Y1 = start.Y;
            Line.X2 = end.X;
            Line.Y2 = end.Y;
        }

        public void UpdateLabel()
        {
            Label.Update();
            PositionLabelBelowCenterOfLine();
        }

        public void PositionLabelBelowCenterOfLine()
        {
            var midPoint = LineUtilities.MidPoint(new Point(Line.X1, Line.Y1),
                new Point(Line.X2, Line.Y2));
            Canvas.SetLeft(Label, midPoint.X);
            Canvas.SetTop(Label, midPoint.Y);
        }
    }

    public class LengthLabel : Label
    {
        private readonly Line _line;
        public LengthLabel(Line line)
        {
            _line = line;
            this.FontSize = 10;
            Update();
        }

        public void Update()
        {
            this.Content = ((int)Math.Round(LineUtilities.Distance(new Point(_line.X1, _line.Y1),
                new Point(_line.X2, _line.Y2)))).ToString();
        }
    }
}