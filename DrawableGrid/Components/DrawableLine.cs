using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using DrawableGrid.Events;
using DrawableGrid.Utilities;

namespace DrawableGrid.Components
{
    public class DrawableLine : Canvas
    {
        public delegate void LineDragEventHandler(DrawableLine source, LineDragEventArgs e);
        public delegate void LineDragReleasedEventHandler(DrawableLine source, MouseButtonEventArgs e);
        public event LineDragEventHandler LineDragged;
        public event LineDragReleasedEventHandler LineDraggedReleased;

        public Line Line { get; }
        protected readonly LengthLabel Label;
        protected bool IsDragging;

        public DrawableLine(Point start, Point end)
            : this(start, end, Brushes.Black) { }

        public DrawableLine(Point start, Point end, Brush brush)
        {
            Line = new Line
            {
                Stroke = brush,
                StrokeThickness = 6,
                X1 = start.X,
                Y1 = start.Y,
                X2 = end.X,
                Y2 = end.Y
            };
            Label = new LengthLabel(Line);
            PositionLabelBelowCenterOfLine();
            Children.Add(Line);
            Children.Add(Label);

            Line.MouseDown += OnLineTarget;
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
            SetLeft(Label, midPoint.X);
            SetTop(Label, midPoint.Y);
        }

        private void OnLineTarget(object source, EventArgs e)
        {
            var resizeGrip = new LineResizeGrip(LineResizeGrip.DragDirection.End);
            Children.Add(resizeGrip);
            resizeGrip.MoveToEndOf(Line);

            resizeGrip.MouseDown += OnLineDrag;
            resizeGrip.MouseLeftButtonUp += OnLineDragRelease;
        }

        public void OnLineDrag(object source, MouseButtonEventArgs e)
        {
            IsDragging = true;
            if (source is LineResizeGrip lineResizeGrip)
            {
                LineDragged?.Invoke(this, new LineDragEventArgs(lineResizeGrip, LineResizeGrip.DragDirection.End));
            }  
        }

        public virtual void OnLineDragRelease(object source, MouseButtonEventArgs e)
        {
            if (!IsDragging) return;
            Children.Remove((UIElement) source);
            LineDraggedReleased?.Invoke(this, e);
            UpdateLabel();
            IsDragging = false;
        }
    }
}