using System;
using Fuel.Windows.Phone.Common;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

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

        private double _barPercentage;
        public double BarPercentage
        {
            get { return _barPercentage; }
            set
            {
                _barPercentage = value;
                RaisePropertyChanged(() => BarPercentage);
            }
        }

        public RelayCommand RefreshCommand { get; set; }

        public MainViewModel()
        {
            //TODO: remove this when done testing
            SmsPercentage = 10;
            CallPercentage = 20;
            BarPercentage = 30;
            RefreshCommand = new RelayCommand(() =>
            {
                CallPercentage = GetRandomNumber(0,100);
                SmsPercentage = GetRandomNumber(0, 100);
                BarPercentage = GetRandomNumber(0, 100);
                //Messenger.Default.Send(new StartStoryboardMessage {StoryboardName = "Loading", LoopForever = true});
            });
        }

        public double GetRandomNumber(double minimum, double maximum)
        {
            var random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
