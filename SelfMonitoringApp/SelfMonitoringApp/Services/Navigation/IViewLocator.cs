using SelfMonitoringApp.ViewModels.Base;
using Xamarin.Forms;

namespace SelfMonitoringApp.Services.Navigation
{
    public interface IViewLocator
    {
        Page CreateAndBindPageFor<TViewModel>(TViewModel viewModel) where TViewModel : INavigationViewModel;
    }
}
