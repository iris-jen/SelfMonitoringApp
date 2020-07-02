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
    public class SubstanceViewModel: NavigatableViewModelBase, INavigationViewModel
    {
        private readonly SubstanceModel _substance;
        private readonly bool _editing;

        public const string NavigationNodeName = "substance";
        public event EventHandler ModelShed;
        public Command SaveLogCommand { get; private set; }

        public string ConsumptionMethod
        {
            get => _substance.ConsumptionMethod;
            set
            {
                if (_substance.ConsumptionMethod == value)
                    return;

                _substance.ConsumptionMethod = value;
                NotifyPropertyChanged();
            }
        }

        public string SubstanceName
        {
            get => _substance.SubstanceName;
            set
            {
                if (_substance.SubstanceName == value)
                    return;

                _substance.SubstanceName = value;
                NotifyPropertyChanged();
            }
        }

        public string Comment
        {
            get => _substance.Comment;
            set
            {
                if (_substance.Comment == value)
                    return;

                _substance.Comment = value;
                NotifyPropertyChanged();
            }
        }

        public double Ammount
        {
            get => _substance.Amount;
            set
            {
                if (_substance.Amount == value)
                    return;

                _substance.Amount = value;
                NotifyPropertyChanged();
            }
        }

        public string Unit
        {
            get => _substance.Unit;
            set
            {
                if (_substance.Unit == value)
                    return;

                _substance.Unit = value;
                NotifyPropertyChanged();
            }
        }

        public double Satisfaction
        {
            get => _substance.Satisfaction;
            set
            {
                if (_substance.Satisfaction == value)
                    return;

                _substance.Satisfaction = value;
                NotifyPropertyChanged();
            }
        }

        public SubstanceViewModel(INavigationService navService, IModel existingModel = null) : base(navService)
        {
            if (existingModel is null)
                _substance = new SubstanceModel();
            else
            {
                _substance = existingModel as SubstanceModel;
                _editing = true;
            }

            SaveLogCommand = new Command(async ()=> await SaveAndPop());
        }

        public IModel RegisterAndGetModel()
        {
            if(!_editing)
                _substance.RegisteredTime = DateTime.Now;

            return _substance;
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
