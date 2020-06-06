using SelfMonitoringApp.Models;
using SelfMonitoringApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SelfMonitoringApp.ViewModels
{
    public class SubstanceDetailViewModel: ExtendedBindableObject, ILogViewModel
    {
        private readonly SubstanceModel _substanceModel;

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

        public SubstanceDetailViewModel()
        {
            _substanceModel = new SubstanceModel();
        }

        public SubstanceDetailViewModel(IModel existingModel)
        {
            _substanceModel = existingModel as SubstanceModel;
        }

        public IModel RegisterAndGetModel()
        {
            _substanceModel.RegisteredTime = DateTime.Now;
            return _substanceModel;
        }
    }
}
