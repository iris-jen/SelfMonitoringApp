using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfMonitoringApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RatingSlider : ContentView
    {
        public double Rating
        {
            get => (double)GetValue(RatingProperty);
            set => SetValue(RatingProperty, value);
        }

        public static BindableProperty RatingProperty = 
            BindableProperty.Create(nameof(Rating) , typeof(double),typeof(RatingSlider), default(double), BindingMode.TwoWay);

        public RatingSlider()
        {
            InitializeComponent();
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Rating = (sender as Slider).Value;
            
        }
    }
}