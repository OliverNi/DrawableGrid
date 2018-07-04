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

        public void Hide()
        {
            Line.Visibility = Visibility.Hidden;
            Label.Visibility = Visibility.Hidden;
        }

        public void Show()
        {
            Line.Visibility = Visibility.Visible;
            Label.Visibility = Visibility.Visible;
        }
    }
}