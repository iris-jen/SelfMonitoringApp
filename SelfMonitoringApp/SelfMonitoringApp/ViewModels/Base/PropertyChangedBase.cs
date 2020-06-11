using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SelfMonitoringApp.ViewModels.Base
{
    public abstract class PropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaiseAllPropertiesChanged()
        {
            OnPropertyChanged(string.Empty);
        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
