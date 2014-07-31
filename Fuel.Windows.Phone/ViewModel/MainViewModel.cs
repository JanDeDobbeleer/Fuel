using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Fuel.Windows.Phone.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private double _smsPercentage;
        public double SmsPercentage
        {
            get { return _smsPercentage; }
            set
            {
                _smsPercentage = value;
                RaisePropertyChanged(()=> SmsPercentage);
            }
        }

        private double _callPercentage;
        public double CallPercentage
        {
            get { return _callPercentage; }
            set
            {
                _callPercentage = value;
                RaisePropertyChanged(() => CallPercentage);
            }
        }

        public RelayCommand RefreshCommand { get; set; }

        public MainViewModel()
        {
            //TODO: remove this when done testing
            SmsPercentage = 33;
            CallPercentage = 66;
            RefreshCommand = new RelayCommand(() =>
            {
                CallPercentage = 70;
                SmsPercentage = 65;
                //Messenger.Default.Send(new StartStoryboardMessage {StoryboardName = "Loading", LoopForever = false});
            });
        }
    }
}
