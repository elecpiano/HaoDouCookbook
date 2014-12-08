using HaoDouCookBook.Common;
using HaoDouCookBook.HaoDou.API;
using HaoDouCookBook.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class UserActivity : UserControl
    {
        #region Dependency Property

        public static readonly DependencyProperty NoItemsTextProperty = DependencyProperty.Register("NoItemsText", typeof(string), typeof(UserActivity), new PropertyMetadata(string.Empty));

        #endregion

        #region Field && CLR Proprety Wrapper

        public string NoItemsText
        {
            get { return (string)GetValue(NoItemsTextProperty); }
            set { SetValue(NoItemsTextProperty, value); }
        }

        private ObservableCollection<UserActivitiesGroup> ActivitiesGroups = new ObservableCollection<UserActivitiesGroup>();

        #endregion

        #region Life Cycle

        public UserActivity()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Public Method

        private void DataBinding()
        {
            this.list.ItemsSource = ActivitiesGroups;
            this.noItemsPanel.DataContext = ActivitiesGroups;
            this.noItemsText.DataContext = this;
        }

        public async Task LoadFirstDataAsync(int userId)
        {
            this.loading.SetState(LoadingState.LOADING);
            DataBinding();
            await UserFeedAPI.GetFollowUserFeed(0, 10, userId, UserGlobal.Instance.GetInt32UserId(), UserGlobal.Instance.UserInfo.Sign, data =>
                {
                    if (data.Activities != null)
                    {
                        ActivitiesGroups.Clear();
                        Dictionary<string, List<UserActivityItem>> dict = new Dictionary<string, List<UserActivityItem>>();
                        foreach (var item in data.Activities)
                        {
                            UserActivityItem usi = new UserActivityItem() {
                                Name = item.Name,
                                Content = item.Content,
                                CreateTime = item.CreateTime,
                                Image = item.Pic,
                                ProductId = item.ItemId
                            };

                            if (dict.ContainsKey(item.FormatTime))
                            {
                                dict[item.FormatTime].Add(usi);
                            }
                            else
                            {
                                List<UserActivityItem> list = new List<UserActivityItem>();
                                list.Add(usi);
                                dict.Add(item.FormatTime, list);
                            }
                        }

                        foreach (var kv in dict)
                        {
                            UserActivitiesGroup group = new UserActivitiesGroup();
                            string[] temp = kv.Key.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                            group.Month = temp[0].Trim();
                            group.Day = temp[1].Trim();

                            foreach (var item in kv.Value)
                            {
                                group.Activities.Add(item);
                            }
                            ActivitiesGroups.Add(group);
                        }
                    }

                    loading.SetState(LoadingState.SUCCESS);

                }, error =>
                {
                    if (Utilities.IsMatchNetworkFail(error.ErrorCode))
                    {
                        loading.RetryAction = async () => await LoadFirstDataAsync(userId);
                        loading.SetState(LoadingState.NETWORK_UNAVAILABLE);
                    }
                    else
                    {
                        loading.SetState(LoadingState.DONE);
                    }
                });
        }

        #endregion

        #region Event

        private void activity_Line_Loaded(object sender, RoutedEventArgs e)
        {
            Utilities.ElementInItemsControl_SetVisibleAtEnd<UserActivityItem>(sender, false);
        }

        private void activitiesGroup_Line_Loaded(object sender, RoutedEventArgs e)
        {
            Utilities.ElementInItemsControl_SetVisibleAtEnd<UserActivitiesGroup>(sender, false);
        }

        #endregion
    }
}
