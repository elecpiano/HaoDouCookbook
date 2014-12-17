using Shared.Infrastructures;
using System.Collections.ObjectModel;

namespace HaoDouCookBook.ViewModels
{
    public class TopicListPageViewModel : BindableBase
    {
        private int count;

        public int Count
        {
            get { return count; }
            set { SetProperty<int>(ref count, value); }
        }

        public ObservableCollection<TopicModel> Topics { get; set; }

        public TopicListPageViewModel()
        {
            Count = 0;
            Topics = new ObservableCollection<TopicModel>();
        }
    }

}
