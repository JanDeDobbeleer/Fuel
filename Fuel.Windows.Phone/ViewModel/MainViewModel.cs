using GalaSoft.MvvmLight;

namespace Fuel.Windows.Phone.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private string _helloWorld;

        public string HelloWorld
        {
            get
            {
                return _helloWorld;
            }
            set
            {
                Set(() => HelloWorld, ref _helloWorld, value);
            }
        }

        public MainViewModel()
        {
            HelloWorld = IsInDesignMode
                ? "Runs in design mode"
                : "Runs in runtime mode";
        }
    }
}
