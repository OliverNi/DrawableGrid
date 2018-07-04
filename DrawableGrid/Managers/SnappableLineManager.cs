using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using DrawableGrid.Components;
using DrawableGrid.Models;

namespace DrawableGrid.Managers
{
    public class SnappableLineManager
    {
        private int _snapDistance = 20;
        private List<SnappableLine> _lines = new List<SnappableLine>();

        public SnappableLine CreateLine(Point start, Point end, int gridSize)
        {
            var line = new SnappableLine(start, end, gridSize);
            SnapLineEndIfItIsCloseToAnotherLine(line, start, end);
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
            line.Move(start, snapPoint);
        }

        private bool IsCloseEnoughToSnap(PointToLineDistance distance)
        {
            return distance.Distance <= _snapDistance;
        }
    }
}