using SelfMonitoringApp.ViewModels.Base;

using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;
using System;


namespace SelfMonitoringApp.Services.Navigation
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
            try
            {
                var page = _viewLocator.CreateAndBindPageFor(viewModel);
                await Navigator.PushAsync(page);
            }
            catch (Exception ex)
            {
                
            }
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
                .OfType<ViewModelBase>()
                .ToArray();

            await Navigator.PopToRootAsync();
        }
    }
}
