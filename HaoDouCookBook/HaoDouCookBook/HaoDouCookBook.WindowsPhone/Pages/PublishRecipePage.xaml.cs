using HaoDouCookBook.Common;
using HaoDouCookBook.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Shared.Utility;
using HaoDouCookBook.Controls;
using Windows.ApplicationModel.Activation;
using HaoDouCookBook.HaoDou.API;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HaoDouCookBook.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PublishRecipePage : Page, IFileOpenPickerContinuable
    {

        #region Page Paremeter Definition

        public class PublishRecipePageParams
        {
            public PublishRecipePageViewModel ViewModel { get; set; }

            public PublishRecipePageParams()
            {

            }
        }

        #endregion

        #region Field && Proprety

        private PublishRecipePageViewModel viewModel;

        #endregion

        #region Life Cycle

        public PublishRecipePage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Required;
            DataBinding();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            if(e.NavigationMode == NavigationMode.Back)
            {
                return;
            }

            PublishRecipePageParams paras = e.Parameter as PublishRecipePageParams;
            if(paras != null)
            {
                viewModel = paras.ViewModel;
                if (viewModel == null)
                {
                    viewModel = new PublishRecipePageViewModel();
                }
            }
            else
            {
                viewModel = new PublishRecipePageViewModel();
            }

            rootScrollViewer.ScrollToVerticalOffset(0);
            UpdateCommandBarState();
            DataBinding();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            base.OnNavigatedFrom(e);
        }

        #endregion

        #region Data Prepare

        private void DataBinding()
        {
            this.root.DataContext = viewModel;
        }

        #endregion

        #region Private Methods

        private void UpdateCommandBarState()
        {
            if (viewModel.IsInStepOne)
            {
                this.next.Visibility = Windows.UI.Xaml.Visibility.Visible;
                this.publish.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else
            {
                this.next.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                this.publish.Visibility = Windows.UI.Xaml.Visibility.Visible; 
            }
        }

        #endregion

        #region Event

        async void HardwareButtons_BackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            if (viewModel.IsInStepOne)
            {
                await Utilities.ShowOKCancelDialog("好豆菜谱", "已经填写的内容将会丢失，您是否要继续离开此页面？", () =>
                {
                    App.Current.RootFrame.GoBack();
                    e.Handled = true;
                }, null);
                e.Handled = true;
            }
            else
            {
                App.Current.RootFrame.GoBack();
                e.Handled = true;
            }

            e.Handled = true;
        }

        #region Step One

        private void Clear_Loaded(object sender, RoutedEventArgs e)
        {
            Utilities.ElementInItemsControl_SetVisibleAtBegin<StuffItem>(sender, false);
        }

        private void AddMainStuff_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (viewModel != null)
            {
                viewModel.MainStuffs.Add(new StuffItem());
            }
        }

        private void AddOtherStuff_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (viewModel != null)
            {
                viewModel.OtherStuffs.Add(new StuffItem());
            }
        }

        private void RemoveMainStuff_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<StuffItem>();
            viewModel.MainStuffs.Remove(dataContext);
        }

        private void RemoveOtherStuff_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<StuffItem>();
            viewModel.OtherStuffs.Remove(dataContext);
        }

        private async void next_click(object sender, RoutedEventArgs e)
        {
            if (!NetworkHelper.Current.IsInternetConnectionAvaiable)
            {
                toast.Show("网络链接不稳定，请稍后再试！");
                return;
            }

            if (string.IsNullOrEmpty(viewModel.RecipeName.Trim()))
            {
                toast.Show("菜谱名不能为空");
                return;
            }

            toast.Show("保存数据中，请稍后...");
            await InfoAPI.SaveInfo(UserGlobal.Instance.UserInfo.Sign,
                UserGlobal.Instance.GetInt32UserId(),
                viewModel.RecipeName,
                viewModel.RecipeId,
                viewModel.GetMainStuffsJson(),
                viewModel.GetOtherStuffsJson(),
                viewModel.Skills,
                viewModel.Intro,
                success => {
                    viewModel.IsInStepOne = false;
                    UpdateCommandBarState();
                    this.rootScrollViewer.ScrollToVerticalOffset(0);

                    if(!UserRecipeDraft.Instance.Recipes.Contains(viewModel))
                    {
                        UserRecipeDraft.Instance.Recipes.Add(viewModel);
                    }

                    UserRecipeDraft.Instance.CommitData();
                },
                error => {
                    if(!NetworkHelper.Current.IsInternetConnectionAvaiable)
                    {
                        toast.Show("网络链接不稳定，请稍后再试！");
                    }
                });

        }

        #endregion

        #region Setp Two

        private void publish_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.RecipeSteps.Count < 4)
            {
                toast.Show("至少需要3个步骤才能发布哦~");
                return;
            }

            if(string.IsNullOrEmpty(viewModel.RecipeCover))
            {
                toast.Show("亲，要先上传封面图才能发布哦~");
                return;
            }


        }

        private void delete_Step_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<PublishRecipeStep>();

            if (dataContext.StepNumber == 0)
            {
                return;
            }

            int index = viewModel.RecipeSteps.IndexOf(dataContext);
            if(index != -1)
            {
                viewModel.RecipeSteps.Remove(dataContext);
                for (int i = index; i < viewModel.RecipeSteps.Count; i++)
                {
                    if(viewModel.RecipeSteps[i].StepNumber > 0)
                    {
                        viewModel.RecipeSteps[i].StepNumber--;
                    }
                }
            }
        }

        private void step_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext<PublishRecipeStep>();
            AddRecipeStepsPage.AddRecipeStepsPageParams paras = new AddRecipeStepsPage.AddRecipeStepsPageParams();
            paras.Step = dataContext;
            paras.CompletedAction = returnStep =>
            {
                dataContext.StepPhoto = returnStep.StepPhoto;
                dataContext.StepIntro = returnStep.StepIntro;

                // StepNumber 为0表示是添加新步骤按钮, 上面步骤让添加界面消失，
                // 所以加入一个新的空step来让添加界面出现。
                //
                if(dataContext.StepNumber == 0)
                {
                    dataContext.StepNumber = viewModel.RecipeSteps.Count;
                    viewModel.RecipeSteps.Add(new PublishRecipeStep());
                }
            };

            App.Current.RootFrame.Navigate(typeof(AddRecipeStepsPage), paras);
        }

       

        private void edit_Tapped(object sender, TappedRoutedEventArgs e)
        {
            viewModel.IsInStepOne = true;
            UpdateCommandBarState();
            this.rootScrollViewer.ScrollToVerticalOffset(0);
        }

        private async void SelectPhoto_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SelectPhotoDialog dialog = new SelectPhotoDialog();
            await dialog.ShowAsync();
        }

        public async void ContinueFileOpenPicker(FileOpenPickerContinuationEventArgs args)
        {
            if (args.Files != null && args.Files.Count > 0)
            {
                var file = args.Files[0];
                viewModel.RecipeCover = await IsolatedStorageHelper.CopyFromFileAsync(file, Constants.PUBLISH_RECIPES_TEMP_FOLDER);
            }
        }

        #endregion

        #endregion
    }
}
