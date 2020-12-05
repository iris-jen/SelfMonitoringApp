using System.Threading.Tasks;

namespace SelfMonitoringApp.ViewModels.Base
{
    public interface INavigationViewModel
    {
        string FullNavigationPath { get; set; }
        Task BeforeFirstShown();
        Task AfterDismissed();
    }
}