﻿using SelfMonitoringApp.ViewModels.Base;
using SelfMonitoringApp.ViewModels.Logs;
using System;
using System.Globalization;
using System.IO;

using Xamarin.Forms;
using Acr.UserDialogs;
using SelfMonitoringApp.Pages;
using System.Threading.Tasks;


namespace SelfMonitoringApp.ViewModels
{
    public class MainViewModel : ViewModelBase, INavigationViewModel
    {
        public Command<PageNames> NavigateCommand { get; set; }

        public MainViewModel()   
        {
            NavigateCommand = new Command<PageNames>((page) => Navigate(page));
        }


        private Task GetNavigatorTask(PageNames page)
        {
            switch (page)
            {
                case PageNames.MoodEditor:
                    return _navigator.NavigateTo(new MoodViewModel());
                case PageNames.SleepEditor:
                    return _navigator.NavigateTo(new SleepViewModel());
                case PageNames.ActivityEditor:
                    return _navigator.NavigateTo(new ActivityViewModel());
                case PageNames.MealEditor:
                    return _navigator.NavigateTo(new MealViewModel());
                case PageNames.SocializationEditor:
                    return _navigator.NavigateTo(new SocializationViewModel());
                case PageNames.SubstanceEditor:
                    return _navigator.NavigateTo(new SubstanceViewModel());
                case PageNames.RawLogViewer:
                    return _navigator.NavigateTo(new DataExplorerViewModel());
                case PageNames.NotificationsViewer:
                    return _navigator.NavigateTo(new NotificationsViewModel());
                case PageNames.GoalsViewer:
                    return _navigator.NavigateTo(new GoalsViewModel());
                case PageNames.Settings:
                    return _navigator.NavigateTo(new SettingsViewModel());
                default:
                    throw new DirectoryNotFoundException($"Cant find directory -- {page}");
            };
        }

        public void Navigate(PageNames page)
        {
            try
            {
                GetNavigatorTask(page).SafeFireAndForget(false);
            }
            catch(Exception ex)
            {
                UserDialogs.Instance.Alert(ex.ToString(), "Oh no i could not navigate :(");
            }
        }
    }
}
