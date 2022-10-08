using System.ComponentModel;

namespace App.Core.ViewModels.Base
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };

        protected void OnPropertyChange(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}