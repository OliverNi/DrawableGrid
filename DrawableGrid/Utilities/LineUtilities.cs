using System;
using System.Windows;
using System.Windows.Shapes;
using DrawableGrid.Models;

namespace DrawableGrid.Utilities
{
    public class LineUtilities
    {
        //Compute the distance from A to B
        public static double Distance(Point pointA, Point pointB)
        {
            var d1 = new Point(pointA.X - pointB.X, pointA.Y - pointB.Y);
            return Math.Sqrt(d1.X * d1.X + d1.Y * d1.Y);
        }

        public static PointToLineDistance GetClosestPointOnLineSegment(Point point, Line line)
        {
            var P = new Vector(point.X, point.Y);
            var A = new Vector(line.X1, line.Y1);
            var B = new Vector(line.X2, line.Y2);
            var AP = P - A;       //Vector from A to P   
            var AB = B - A;       //Vector from A to B  

            var magnitudeAB = AB.LengthSquared;     //Magnitude of AB vector (it's length squared)     
            var ABAPproduct = Vector.Multiply(AP, AB);    //The DOT product of a_to_p and a_to_b     
            var distance = ABAPproduct / magnitudeAB; //The normalized "distance" from a to your closest point  

            if (distance < 0)     //Check if P projection is over vectorAB     
            {
                var pointOnLine = AsPoint(A);
                return new PointToLineDistance(Distance(point, pointOnLine), pointOnLine);
            }
            if (distance > 1)
            {
                var pointOnLine = AsPoint(B);
                return new PointToLineDistance(Distance(point, pointOnLine), pointOnLine);
            }
            else
            {
                var pointOnLine = AsPoint(A + AB * distance);
                return new PointToLineDistance(Distance(point, pointOnLine), pointOnLine);
            }
        }

        private static Point AsPoint(Vector vector)
        {
            return new Point(vector.X, vector.Y);
        }
    }
}