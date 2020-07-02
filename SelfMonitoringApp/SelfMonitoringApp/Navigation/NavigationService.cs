using SelfMonitoringApp.Models;
using SelfMonitoringApp.ViewModels.Base;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SelfMonitoringApp.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly IHaveMainPage _presentationRoot;
        private readonly IViewLocator _viewLocator;

        public NavigationService(IHaveMainPage presentationRoot, IViewLocator viewLocator)
        {
            _presentationRoot = presentationRoot;
            _viewLocator = viewLocator;
        }

        private INavigation Navigator => _presentationRoot.MainPage.Navigation;

        public void PresentAsMainPage(INavigationViewModel viewModel)
        {
            var page = _viewLocator.CreateAndBindPageFor(viewModel);
            _presentationRoot.MainPage = page;
        }

        public void PresentAsNavigatableMainPage(INavigationViewModel viewModel)
        {
            var page = _viewLocator.CreateAndBindPageFor(viewModel);
            NavigationPage newNavigationPage = new NavigationPage(page);
            _presentationRoot.MainPage = newNavigationPage;
        }
        
        public async Task NavigateTo(INavigationViewModel viewModel)
        {
            var page = _viewLocator.CreateAndBindPageFor(viewModel);
            await Navigator.PushAsync(page);
        }

        public async Task NavigateBack()
        {       
            await Navigator.PopAsync();
        }

        public async Task NavigateBackToRoot()
        {
            var toDismiss = Navigator
                .NavigationStack
                .Skip(1)
                .Select(vw => vw.BindingContext)
                .OfType<NavigatableViewModelBase>()
                .ToArray();

            await Navigator.PopToRootAsync();
        }
    }
}
