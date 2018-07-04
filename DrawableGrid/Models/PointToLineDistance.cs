using System.Windows;

namespace DrawableGrid.Models
{
    public class PointToLineDistance
    {
        public int Distance { get; }
        public Point ClosestPointInLine { get; }

        public PointToLineDistance(int distance, Point closestPointInLine)
        {
            Distance = distance;
            ClosestPointInLine = closestPointInLine;
        }
    }
}