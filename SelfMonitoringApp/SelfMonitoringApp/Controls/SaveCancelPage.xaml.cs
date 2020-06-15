using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            BindableProperty.Create("SaveCommand", typeof(Command), typeof(SaveCancelPage), default(Command));

        public Command CancelCommand
        {
            get => (Command)GetValue(CancelCommandProperty);
            set => SetValue(CancelCommandProperty, value);
        }

        public static readonly BindableProperty CancelCommandProperty =
            BindableProperty.Create("CancelCommand", typeof(Command), typeof(SaveCancelPage), default(Command));
    }
}