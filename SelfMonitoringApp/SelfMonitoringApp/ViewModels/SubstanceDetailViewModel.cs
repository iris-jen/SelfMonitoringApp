using SelfMonitoringApp.Models;
using SelfMonitoringApp.Navigation;
using SelfMonitoringApp.Services;
using SelfMonitoringApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SelfMonitoringApp.ViewModels
{
    public class SubstanceViewModel: NavigatableViewModelBase, INavigationViewModel
    {
        private readonly SubstanceModel _substanceModel;
        public const string NavigationNodeName = "substance";

        public Command SaveLogCommand { get; private set; }

        public string ConsumptionMethod
        {
            get => _substanceModel.ConsumptionMethod;
            set
            {
                if (_substanceModel.ConsumptionMethod == value)
                    return;

                _substanceModel.ConsumptionMethod = value;
                NotifyPropertyChanged();
            }
        }

        public string SubstanceName
        {
            get => _substanceModel.SubstanceName;
            set
            {
                if (_substanceModel.SubstanceName == value)
                    return;

                _substanceModel.SubstanceName = value;
                NotifyPropertyChanged();
            }
        }

        public string Comment
        {
            get => _substanceModel.Comment;
            set
            {
                if (_substanceModel.Comment == value)
                    return;

                _substanceModel.Comment = value;
                NotifyPropertyChanged();
            }
        }

        public double Ammount
        {
            get => _substanceModel.Amount;
            set
            {
                if (_substanceModel.Amount == value)
                    return;

                _substanceModel.Amount = value;
                NotifyPropertyChanged();
            }
        }

        public string Unit
        {
            get => _substanceModel.Unit;
            set
            {
                if (_substanceModel.Unit == value)
                    return;

                _substanceModel.Unit = value;
                NotifyPropertyChanged();
            }
        }

        public SubstanceViewModel(INavigationService navService, IModel existingModel = null) : base(navService)
        {
            if (existingModel is null)
                _substanceModel = new SubstanceModel();
            else
                _substanceModel = existingModel as SubstanceModel;

            SaveLogCommand = new Command(SaveAndPop);
        }

        public IModel RegisterAndGetModel()
        {
            _substanceModel.RegisteredTime = DateTime.Now;
            return _substanceModel;
        }

        public void SaveAndPop()
        {
            DataStore.AddModel(RegisterAndGetModel());
            _navigator.NavigateBack();
        }
    }
}
