using SelfMonitoringApp.ViewModels.Base;
using System;
using Xamarin.Forms;

namespace SelfMonitoringApp.Services.Navigation
{
    public class ViewLocator : IViewLocator
    {
        public Page CreateAndBindPageFor<TViewModel>(TViewModel viewModel) where TViewModel : INavigationViewModel
        {
            try
            {
                var pageType = FindPageForViewModel(viewModel.GetType());

                var page = (Page)Activator.CreateInstance(pageType);

                page.BindingContext = viewModel;

                return page;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        protected virtual Type FindPageForViewModel(Type viewModelType)
        {
            var pageTypeName = viewModelType
                .AssemblyQualifiedName
                .Replace("ViewModel", "Page");

            var pageType = Type.GetType(pageTypeName);
            if (pageType == null)
                throw new ArgumentException(pageTypeName + " type does not exist");

            return pageType;
        }
    }
}
