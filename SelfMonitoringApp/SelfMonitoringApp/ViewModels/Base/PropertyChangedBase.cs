using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SelfMonitoringApp.ViewModels.Base
{
    // from
    // https://github.com/RockSolidKnowledge/blog-viewmodel-first-navigation
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
          
        }


    }
}
