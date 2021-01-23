using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfMonitoringApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SaveCancelPage : ContentView
    {
        public SaveCancelPage()
        {
            InitializeComponent();
        }

        public Command SaveCommand
        {
            get => (Command)GetValue(SaveCommandProperty);
            set => SetValue(SaveCommandProperty, value);
        }

        public static readonly BindableProperty SaveCommandProperty =
            BindableProperty.Create(nameof(SaveCommand), typeof(Command), typeof(SaveCancelPage), default(Command));

        public Command CancelCommand
        {
            get => (Command)GetValue(CancelCommandProperty);
            set => SetValue(CancelCommandProperty, value);
        }

        public static readonly BindableProperty CancelCommandProperty =
            BindableProperty.Create(nameof(CancelCommand), typeof(Command), typeof(SaveCancelPage), default(Command));

        public string PageName
        {
            get => GetValue(PageNameProperty).ToString();
            set => SetValue(PageNameProperty, value);
        }

        public static readonly BindableProperty PageNameProperty =
            BindableProperty.Create(nameof(PageName), typeof(string), typeof(SaveCancelPage), string.Empty);
    }
}