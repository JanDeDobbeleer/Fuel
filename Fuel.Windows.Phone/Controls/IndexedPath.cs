using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Fuel.Windows.Phone.Controls
{
    public class IndexedPath:Path
    {
        public static readonly DependencyProperty SelectedIndexBindingProperty =
            DependencyProperty.Register("SelectedIndex", typeof(int), typeof(IndexedPath), new PropertyMetadata(0, OnSelectedIndexChanged));

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexBindingProperty); }
            set { SetValue(SelectedIndexBindingProperty, value); }
        }

        private static void OnSelectedIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((IndexedPath)d).Fill = ((int)e.NewValue == ((IndexedPath)d).Index) ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.LightGray);
        }

        private int _index;
        public int Index
        {
            get { return _index; }
            set
            {
                _index = value;
                Fill = (value == SelectedIndex) ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.LightGray);
            }
        }
    }
}
