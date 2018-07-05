using System.Collections.Generic;
using System.Windows;
using DrawableGrid.Components;
using DrawableGrid.Models;

namespace DrawableGrid.Managers
{
    public class SnappableLineManager
    {
        private const double SnapDistance = 20;
        private readonly List<SnappableLine> _lines = new List<SnappableLine>();
        public SnappableLine TargetedLine { get; set; }

        public SnappableLine CreateLine(Point start, Point end, int gridSize)
        {
            var line = new SnappableLine(start, end, gridSize);
            SnapLineEndIfItIsCloseToAnotherLine(line, start, end);
            _lines.Add(line);
            return line;
        }

        public void MoveLine(SnappableLine line, Point start, Point end)
        {
            SnapLineEndIfItIsCloseToAnotherLine(line, start, end);
        }

        private void SnapLineEndIfItIsCloseToAnotherLine(SnappableLine line, Point start, Point end)
        {
            foreach (var existingLine in _lines)
            {
                var distance = existingLine.DistanceFrom(end);
                if (!IsCloseEnoughToSnap(distance)) continue;

                SnapLineEndToPoint(line, start, distance.ClosestPointInLine);
                return;
            }

            line.Move(start, end);
        }

        private void SnapLineEndToPoint(SnappableLine line, Point start, Point snapPoint)
        {
            line.Move(start, snapPoint, false);
        }

        private bool IsCloseEnoughToSnap(PointToLineDistance distance)
        {
            return distance.Distance <= SnapDistance;
        }
    }
}