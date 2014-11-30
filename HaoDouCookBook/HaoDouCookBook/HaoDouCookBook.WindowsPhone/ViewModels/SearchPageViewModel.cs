using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
