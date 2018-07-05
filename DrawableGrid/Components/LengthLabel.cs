using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using DrawableGrid.Utilities;

namespace DrawableGrid.Components
{
    public class LengthLabel : Label
    {
        private readonly Line _line;
        public LengthLabel(Line line)
        {
            _line = line;
            FontSize = 10;
            Update();
        }

        public void Update()
        {
            Content = ((int)Math.Round(LineUtilities.Distance(new Point(_line.X1, _line.Y1),
                new Point(_line.X2, _line.Y2)))).ToString();
        }
    }
}