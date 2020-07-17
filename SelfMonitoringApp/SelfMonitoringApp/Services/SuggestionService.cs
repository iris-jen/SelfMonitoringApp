using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SelfMonitoringApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Forms;

namespace SelfMonitoringApp.Services
{
    public class SuggestionService : ISuggestionService
    {
        [JsonConverter(typeof(StringEnumConverter))]
        private Dictionary<SuggestionTypes, List<string>> _suggestions;

        public const string SuggestionsFilename = "Suggestions.json";
        public static string FilePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                return Path.Combine(basePath, SuggestionsFilename);
            }
        }

        public void Save() => File.WriteAllText(FilePath, JsonConvert.SerializeObject(_suggestions, Formatting.Indented));

        public SuggestionService()
        {
            _suggestions = new Dictionary<SuggestionTypes, List<string>>();

            if (File.Exists(FilePath))
            {
    
                _suggestions = JsonConvert.DeserializeObject<Dictionary<SuggestionTypes, List<string>>>(File.ReadAllText(FilePath));
            }
            
            foreach (var suggestionType in Enum.GetValues(typeof(SuggestionTypes)))
            {
                if(!_suggestions.ContainsKey((SuggestionTypes)suggestionType))
                    _suggestions.Add((SuggestionTypes)suggestionType, new List<string>());
            }
        }

        public ObservableCollection<SuggestionModel> GetSuggestionCollection(SuggestionTypes type)
        {
            var output = new ObservableCollection<SuggestionModel>();

            foreach (var suggestion in _suggestions[type])
                output.Add(new SuggestionModel() { SuggestionText = suggestion });

            return output;
        }
       
        public void AddSuggestion(SuggestionTypes type, string suggestion)
        {
            if (!_suggestions[type].Contains(suggestion))
            {
                _suggestions[type].Add(suggestion);
                Save();
            }
        }

        public void RemoveSuggestion(SuggestionTypes types, string suggestion)
        {
            if (_suggestions[types].Contains(suggestion))
            {
                _suggestions[types].Remove(suggestion);
                Save();
            }
        }
    }
}
