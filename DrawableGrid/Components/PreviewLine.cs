using System.Windows;
using System.Windows.Media;

namespace DrawableGrid.Components
{
    public sealed class PreviewLine : SnappableLine
    {
        public PreviewLine(int gridSize) 
            : base(new Point(0, 0), new Point(0, 0), gridSize, Brushes.Gray)
        {
            Hide();
        }
    }
}