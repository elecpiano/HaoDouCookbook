using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Shared.Utility;
using HaoDouCookBook.Common;
using HaoDouCookBook.Pages;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class LocalDownloadsControl : UserControl
    {
        public Action<LocalCateogoryFolder> OnFolderTapped { get; set; }

        public Action CreatNewAction { get; set; }

        public static readonly DependencyProperty EditEnabledProperty = DependencyProperty.Register("EditEnabled", typeof(bool), typeof(LocalDownloadsControl), new PropertyMetadata(true));

        public bool EditEnabled
        {
            get { return (bool)GetValue(EditEnabledProperty); }
            set { SetValue(EditEnabledProperty, value); }
        }

        public LocalDownloadsControl()
        {
            this.InitializeComponent();
            DataBinding();
        }

        public void UpdateViewModel()
        {
            DataBinding();
        }

        private void DataBinding()
        {
            this.root.DataContext = LocalDownloads.Instance;
        }

        private void SmartGrid_Holding(object sender, HoldingRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<LocalCateogoryFolder>();
            int index = LocalDownloads.Instance.Folders.IndexOf(dataContext);

            // dont' show the context menu when holding on default category
            //
            if (dataContext.FolderName != LocalCateogoryFolder.SYSTEM_FOLDER_NAME && index != 0)
            {
                sender.ShowFlayout();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<LocalCateogoryFolder>();
            BigTextBox.BigTextBoxParams paras = new BigTextBox.BigTextBoxParams();
            paras.MaxLength = 20;
            paras.Text = dataContext.FolderName;
            paras.ConfirmAction = (newName) =>
            {
                if (string.IsNullOrEmpty(newName))
                {
                    toast.Show("分类名不能为空");
                    return;
                }

                dataContext.FolderName = newName;
                LocalDownloads.Instance.CommitData();
            };

            App.Current.RootFrame.Navigate(typeof(BigTextBox), paras);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<LocalCateogoryFolder>();
            LocalDownloads.Instance.Folders.Remove(dataContext);
            LocalDownloads.Instance.CommitData();
        }

        private void CreateNew_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (CreatNewAction != null)
            {
                CreatNewAction.Invoke();
            }
        }

        private void Folder_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (OnFolderTapped != null)
            {
                OnFolderTapped.Invoke(sender.GetDataContext<LocalCateogoryFolder>());
            }
        }

        private void EditMenu_Loaded(object sender, RoutedEventArgs e)
        {
            FrameworkElement fe = sender as FrameworkElement;
            if(fe != null)
            {
                fe.Visibility = EditEnabled ? Windows.UI.Xaml.Visibility.Visible : Windows.UI.Xaml.Visibility.Collapsed;
            }
        }
    }
}
