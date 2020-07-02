using SelfMonitoringApp.Models;
using SelfMonitoringApp.Navigation;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels
{
    public class MealViewModel: NavigatableViewModelBase, INavigationViewModel
    {
        private readonly MealModel _mealModel;
        public const string NavigationNodeName = "meal";
        public event EventHandler ModelShed;
        private bool _editing;

        public Command SaveLogCommand { get; private set; }

        public string MealSize
        {
            get => _mealModel.MealSize;
            set
            {
                if (_mealModel.MealSize == value)
                    return;

                _mealModel.MealSize = value;
                NotifyPropertyChanged();
            }
        }

        public string MealType
        {
            get => _mealModel.MealType;
            set
            {
                if (_mealModel.MealType == value)
                    return;

                _mealModel.MealType = value;
                NotifyPropertyChanged();
            }
        }

        public string Description
        {
            get => _mealModel.Description;
            set
            {
                if (_mealModel.Description == value)
                    return;

                _mealModel.Description = value;
                NotifyPropertyChanged();
            }
        }

        public double Satisfaction
        {
            get => _mealModel.Satisfaction;
            set
            {
                if (_mealModel.Satisfaction == value)
                    return;

                _mealModel.Satisfaction = value;
                NotifyPropertyChanged();
            }
        }

        public MealViewModel(INavigationService navService, IModel existingMeal = null)
            :base(navService)
        {
            if (existingMeal is null)
                _mealModel = new MealModel();
            else
            {
                _mealModel = existingMeal as MealModel;
                _editing = true;
            }

            SaveLogCommand = new Command(async () => await SaveAndPop());
        }

        public IModel RegisterAndGetModel()
        {
            if(!_editing)
                _mealModel.RegisteredTime = DateTime.Now;

            return _mealModel;
        }

        public async Task SaveAndPop()
        {
            var model = RegisterAndGetModel();
            await App.Database.AddOrModifyModelAsync(model);
            await _navigator.NavigateBack();
            ModelShed?.Invoke(this, new ModelShedEventArgs(model));
        }
    }
}
