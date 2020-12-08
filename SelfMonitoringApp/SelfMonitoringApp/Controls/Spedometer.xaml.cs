using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Runtime.CompilerServices;

namespace SelfMonitoringApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Spedometer : ContentView
    {
        public double Rating
        {
            get => (double)GetValue(RatingProperty);
            set => SetValue(RatingProperty, value);
        }

        public static BindableProperty RatingProperty = 
            BindableProperty.Create(nameof(Rating) , typeof(double),typeof(Spedometer), default(double), BindingMode.TwoWay);

        public Spedometer()
        {
            InitializeComponent();
        }

    }
}