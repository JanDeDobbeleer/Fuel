using System;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace Fuel.Windows.Phone.Controls
{
    public class SplitPanel : Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            // the final measure size is the available size for the width, and the maximum
            // desired size of our children for the height
            var finalSize = new Size {Width = availableSize.Width};

            if (Children.Count != 0)
                availableSize.Width /= (double)Children.Count;

            foreach (var current in Children)
            {
                current.Measure(availableSize);

                var desiredSize = current.DesiredSize;
                finalSize.Height = Math.Max(finalSize.Height, desiredSize.Height);
            }

            // make sure it will works in design time mode
            if (double.IsPositiveInfinity(finalSize.Height) || double.IsPositiveInfinity(finalSize.Width))
                return Size.Empty;

            return finalSize;
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            var finalRect = new Rect(new Point(), arrangeSize);
            var width = arrangeSize.Width/Children.Count;

            foreach (var child in Children)
            {
                finalRect.Height = Math.Max(arrangeSize.Height, child.DesiredSize.Height);
                finalRect.Width = width;

                child.Arrange(finalRect);

                // move each child by the width increment 
                finalRect.X += width;
            }

            return arrangeSize;
        }
    }
}
