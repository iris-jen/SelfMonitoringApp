using SelfMonitoringApp.Models.Base;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfMonitoringApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogView : ContentView
    {
        public LogView()
        {
            InitializeComponent();
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(LogView), default(string));

        public bool Show
        {
            get => (bool)GetValue(ShowProperty);
            set => SetValue(ShowProperty, value);
        }

        public static readonly BindableProperty ShowProperty =
            BindableProperty.Create(nameof(Show), typeof(bool), typeof(LogView), default(bool));

        public ModelType ModelType
        {
            get => (ModelType)GetValue(ModelTypeProperty);
            set => SetValue(ModelTypeProperty, value);
        }

        public static readonly BindableProperty ModelTypeProperty =
            BindableProperty.Create(nameof(ModelType), typeof(ModelType), typeof(LogView), default(ModelType));

        public Command<ModelType> EditCommand
        {
            get => (Command<ModelType>)GetValue(EditCommandProperty);
            set => SetValue(EditCommandProperty, value);
        }

        public static readonly BindableProperty EditCommandProperty =
            BindableProperty.Create(nameof(EditCommand), typeof(Command<ModelType>), typeof(LogView), default(Command<ModelType>));

        public Command<ModelType> DeleteCommand
        {
            get => (Command<ModelType>)GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }

        public static readonly BindableProperty DeleteCommandProperty =
            BindableProperty.Create(nameof(DeleteCommand), typeof(Command<ModelType>), typeof(LogView), default(Command<ModelType>));

        public Command<ModelType> AddCommand
        {
            get => (Command<ModelType>)GetValue(AddCommandProperty);
            set => SetValue(AddCommandProperty, value);
        }

        public static readonly BindableProperty AddCommandProperty =
            BindableProperty.Create(nameof(AddCommand), typeof(Command<ModelType>), typeof(LogView), default(Command<ModelType>));
    }
}