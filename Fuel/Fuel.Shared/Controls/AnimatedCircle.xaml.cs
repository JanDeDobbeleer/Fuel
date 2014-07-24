using System.ComponentModel;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Fuel.Controls
{
    public sealed partial class AnimatedCircle : UserControl, INotifyPropertyChanged
    {
        #region dependency property
        public static readonly DependencyProperty PercentageProperty =
            DependencyProperty.Register("Percentage", typeof(double), typeof(AnimatedCircle), new PropertyMetadata(65d, OnPercentageChanged));

        // Using a DependencyProperty as the backing store for StrokeThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(int), typeof(AnimatedCircle), new PropertyMetadata(5));

        // Using a DependencyProperty as the backing store for SegmentColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SegmentColorProperty =
            DependencyProperty.Register("SegmentColor", typeof(Brush), typeof(AnimatedCircle), new PropertyMetadata(new SolidColorBrush(Colors.Yellow)));

        // Using a DependencyProperty as the backing store for Radius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadiusProperty =
            DependencyProperty.Register("Radius", typeof(int), typeof(AnimatedCircle), new PropertyMetadata(25, OnPropertyChanged));

        // Using a DependencyProperty as the backing store for Angle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(double), typeof(AnimatedCircle), new PropertyMetadata(120d, OnPropertyChanged));

        // Using a DependencyProperty as the backing store for the path width.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CirlceWidthProperty =
            DependencyProperty.Register("CirlceWidth", typeof(double), typeof(AnimatedCircle), new PropertyMetadata(120d, OnPropertyChanged));
        
        // Using a DependencyProperty as the backing store for the path height.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CircleHeightProperty =
            DependencyProperty.Register("CircleHeight", typeof(double), typeof(AnimatedCircle), new PropertyMetadata(120d, OnPropertyChanged));
        #endregion

        public AnimatedCircle()
        {
            InitializeComponent();
        }

        #region properties
        public int Radius
        {
            get { return (int)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }

        public Brush SegmentColor
        {
            get { return (Brush)GetValue(SegmentColorProperty); }
            set { SetValue(SegmentColorProperty, value); }
        }

        public int StrokeThickness
        {
            get { return (int)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        public double Percentage
        {
            get { return (double)GetValue(PercentageProperty); }
            set { SetValue(PercentageProperty, value); }
        }

        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        public double CirlceWidth
        {
            get { return (double)GetValue(CirlceWidthProperty); }
            set { SetValue(CirlceWidthProperty, value); }
        }

        public double CircleHeight
        {
            get { return (double)GetValue(CircleHeightProperty); }
            set { SetValue(CircleHeightProperty, value); }
        }
        #endregion

        // Using a DependencyProperty as the backing store for Percentage.  This enables animation, styling, binding, etc...

        private static void OnPercentageChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var circle = sender as AnimatedCircle;
            if(circle == null)
                return;
            var angle = (circle.Percentage * 360) / 100;
            AnimationHelper.AnimatePath(circle.ProgressPath, circle.Radius, new Point(circle.Radius, 0), angle);
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private static void OnPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var circle = sender as AnimatedCircle;
            if (circle == null)
                return;
            var h = circle.PropertyChanged;
            if (h != null)
            {
                h(circle, new PropertyChangedEventArgs(e.Property.ToString()));
            }
        }
    }
}
