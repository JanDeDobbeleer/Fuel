using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556
using Fuel.Windows.Phone.Common;
using GalaSoft.MvvmLight.Messaging;

namespace Fuel.Windows.Phone.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            Messenger.Default.Register<StartStoryboardMessage>(this, x => StartStoryboard(x.StoryboardName, x.LoopForever));
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
        
        private void StartStoryboard(string storyboardName, bool loopForever)
        {
            var storyboard = FindName(storyboardName) as Storyboard;
            if (storyboard == null) 
                return;
            storyboard.RepeatBehavior = loopForever ? RepeatBehavior.Forever : new RepeatBehavior(1);
            CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                storyboard.Begin();
            });
            
        }
    }
}
