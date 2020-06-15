using SelfMonitoringApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SelfMonitoringApp.ViewModels
{
    public class DaySummaryViewModel
    {
        public ObservableCollection<MealModel> Meals { get; private set; }

        public ObservableCollection<SubstanceModel> Substances { get; private set; }

    }
}
