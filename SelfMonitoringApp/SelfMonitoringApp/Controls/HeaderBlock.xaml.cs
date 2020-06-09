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
    public partial class HeaderBlock:ContentView
    {
        public HeaderBlock()
        {
            InitializeComponent();
        }

        public string HeaderText
        {
            get =>(string)GetValue(HeaderTextProperty);
            set => SetValue(HeaderTextProperty, value);
        }

        // Using a DependencyProperty as the backing store for HeaderText.  This enables animation, styling, binding, etc...
        public static readonly BindableProperty HeaderTextProperty =
            BindableProperty.Create("HeaderText", typeof(string), typeof(HeaderBlock), default(string));
    }
}