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
    public partial class RatingSlider : ContentView
    {
        public double SliderValue
        {
            get => (double)GetValue(SliderValueProperty);
            set => SetValue(SliderValueProperty, value);
        }

        public static BindableProperty SliderValueProperty = 
            BindableProperty.Create("SliderValue", typeof(double),typeof(RatingSlider), default(double));

        public RatingSlider()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}