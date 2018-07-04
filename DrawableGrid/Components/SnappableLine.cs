
using System.Windows;
using System.Windows.Media;
using DrawableGrid.Models;

namespace DrawableGrid.Components
{
    public class SnappableLine : DrawableLine
    {
        private readonly int _gridSize;

        public SnappableLine(Point start, Point end, int gridSize) :
            base(SnappedPointOf(start, gridSize), SnappedPointOf(end, gridSize))
        {
            _gridSize = gridSize;
        }

        public SnappableLine(Point start, Point end, int gridSize, Brush brush) :
            base(SnappedPointOf(start, gridSize), SnappedPointOf(end, gridSize), brush)
        {
            _gridSize = gridSize;
        }

        public override void Move(Point start, Point end)
        {
            base.Move(SnappedPointOf(start, _gridSize), SnappedPointOf(end, _gridSize));
        }

        public PointToLineDistance DistanceFrom(Point point)
        {
            //@TODO calculate shortest distance from point to line
            return null;
        }

        private static Point SnappedPointOf(Point point, int gridSize)
        {
            var xSnap = point.X % gridSize;
            var ySnap = point.Y % gridSize;

            // If it's less than half the grid size, snap left/up 
            // (by subtracting the remainder), 
            // otherwise move it the remaining distance of the grid size right/down
            // (by adding the remaining distance to the next grid point).
            if (xSnap <= gridSize / 2.0)
                xSnap *= -1;
            else
                xSnap = gridSize - xSnap;
            if (ySnap <= gridSize / 2.0)
                ySnap *= -1;
            else
                ySnap = gridSize - ySnap;

            xSnap += point.X;
            ySnap += point.Y;
            return new Point(xSnap, ySnap);
        }
    }
}