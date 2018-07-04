using System.Windows;

namespace DrawableGrid.Models
{
    public class PointToLineDistance
    {
        public double Distance { get; }
        public Point ClosestPointInLine { get; }

        public PointToLineDistance(double distance, Point closestPointInLine)
        {
            Distance = distance;
            ClosestPointInLine = closestPointInLine;
        }
    }
}