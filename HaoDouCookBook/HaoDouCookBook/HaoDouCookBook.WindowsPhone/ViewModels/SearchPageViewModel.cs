using Shared.Infrastructures;
using System.Collections.ObjectModel;

namespace HaoDouCookBook.ViewModels
{
    public class SearchPageViewModel : BindableBase
    {
        public ObservableCollection<string> SearchLogs { get; set; }

        public ObservableCollection<string> HotSearchKeywords { get; set; }

        public SearchPageViewModel()
        {
            SearchLogs = new ObservableCollection<string>();
            HotSearchKeywords = new ObservableCollection<string>();
        }
    }

}
