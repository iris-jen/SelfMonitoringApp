using SelfMonitoringApp.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace SelfMonitoringApp.ViewModels.Base
{
    public class NavigatableViewModelBase : PropertyChangedBase
    {
        protected readonly INavigationService _navigator;
        public Command BackCommand { get; private set; }
        public Command HomeCommand { get; private set; }

        public string FullNavigationPath { get; set; }

        public NavigatableViewModelBase(INavigationService navigator)
        {
            _navigator = navigator;
            BackCommand = new Command(async () => await _navigator.NavigateBack());
            HomeCommand = new Command(async () => await _navigator.NavigateBackToRoot());  
        }
    }
}
