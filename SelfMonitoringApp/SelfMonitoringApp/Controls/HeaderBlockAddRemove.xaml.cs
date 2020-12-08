using Acr.UserDialogs;
using SelfMonitoringApp.Services;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SelfMonitoringApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeaderBlockAddRemove : Frame
    {
        public ObservableCollection<string> SuggestionItems { get; private set; }
        public ISuggestionService SuggestionService;

        public string SelectedSuggestion
        {
            get { return (string)GetValue(SelectedSuggestionProperty); }
            set { SetValue(SelectedSuggestionProperty, value); }
        }

        public static readonly BindableProperty SelectedSuggestionProperty =
            BindableProperty.Create(nameof(SelectedSuggestion), typeof(string), typeof(HeaderBlockAddRemove), 
                default(string), BindingMode.TwoWay, propertyChanged: HandleSuggestionChanged);

        public bool InitialSet;

        private static void HandleSuggestionChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var block = (HeaderBlockAddRemove)bindable;
            
            if (newValue is null)
                return;

            if (!block.InitialSet)
            {
                var type = newValue.ToString();

                if (type == string.Empty)
                    return;

                if (!block.SuggestionItems.Contains(type))
                {
                    block.SuggestionService.AddSuggestion(block.BoxType, type);
                    block.SuggestionItems.Add(type);
                }

                block.HeaderPicker.SelectedIndex = block.SuggestionItems.IndexOf(type);
                block.InitialSet = true;
            }
        }

        public string Title
        {
            get { return HeaderLabel.Text; }
            set { HeaderLabel.Text = value; }
        }

        public static readonly BindableProperty BoxTypeProperty =
            BindableProperty.Create(nameof(BoxType), typeof(SuggestionTypes), typeof(HeaderBlockAddRemove), 
                SuggestionTypes.Invalid, BindingMode.TwoWay, propertyChanged: HandleBoxTypeChanged);

        public SuggestionTypes BoxType
        {
            get { return (SuggestionTypes)GetValue(BoxTypeProperty); }
            set { SetValue(BoxTypeProperty,value); }
        }

        private static void HandleBoxTypeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var block = (HeaderBlockAddRemove)bindable;
            var type = (SuggestionTypes)newValue;
            block.BoxType = type;

            block.SuggestionItems = block.SuggestionService.GetSuggestionCollection(type);
            
            block.HeaderPicker.ItemsSource = block.SuggestionItems;
            block.HeaderPicker.SelectedIndex = block.SuggestionItems.IndexOf(block.SelectedSuggestion);

        }

        public HeaderBlockAddRemove()
        {
            SuggestionService = (ISuggestionService)Locator.Current.GetService(typeof(ISuggestionService));

            InitializeComponent();
            AddButton.Command = new Command(async () => await AddItem());
        }

        private async Task AddItem()
        {
            string promptStr = string.Empty;
            switch (BoxType)
            {
                case SuggestionTypes.ActivityNames:
                    promptStr = "Enter a new activity name";
                    break;
                case SuggestionTypes.Emotions:
                    promptStr = "Enter a new emotion";
                    break;
                case SuggestionTypes.Locations:
                    promptStr = "Enter a new location";
                    break;
                case SuggestionTypes.MealNames:
                    promptStr = "Enter a meal name";
                    break;
                case SuggestionTypes.MealSizes:
                    promptStr = "Enter a new meal size";
                    break;
                case SuggestionTypes.MealTypes:
                    promptStr = "Enter a new type of meal";
                    break;
                case SuggestionTypes.SubstanceConsumptionMethods:
                    promptStr = "Enter a new consumption method";
                    break;
                case SuggestionTypes.SubstanceNames:
                    promptStr = "Enter a new substance name";
                    break;
                case SuggestionTypes.Units:
                    promptStr = "Enter a new unit";
                    break;
                case SuggestionTypes.SocializationTypes:
                    promptStr = "How did you socialize?";
                    break;
            }


            var res = await UserDialogs.Instance.PromptAsync(new PromptConfig() { Message = promptStr });
            var userInput = res.Text;

            if (!res.Ok)
                return;

            SuggestionItems.Add(userInput);
            SuggestionService.AddSuggestion(BoxType, userInput);
            HeaderPicker.SelectedIndex = SuggestionItems.IndexOf(userInput);
        }

        private void RemoveButton_Pressed(object sender, EventArgs e)
        {
            if (HeaderPicker.SelectedItem == null)
                return;

            var selectedItem = HeaderPicker.SelectedItem.ToString();

            SuggestionService.RemoveSuggestion(BoxType, selectedItem);
            SuggestionItems.Remove(selectedItem);
        }

        private void HeaderPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HeaderPicker.SelectedIndex == -1)
                return;

            SelectedSuggestion = SuggestionItems[HeaderPicker.SelectedIndex];
        }
    }
}