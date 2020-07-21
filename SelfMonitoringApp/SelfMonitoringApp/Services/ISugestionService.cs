using SelfMonitoringApp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SelfMonitoringApp.Services
{
    public interface ISuggestionService
    {
        ObservableCollection<string> GetSuggestionCollection(SuggestionTypes type);

        void AddSuggestion(SuggestionTypes type, string sugestion);

        void RemoveSuggestion(SuggestionTypes types, string suggestion);
    }
}
