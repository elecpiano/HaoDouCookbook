using HaoDouCookBook.Pages;
using HaoDouCookBook.ViewModels;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Shared.Utility;
using Windows.UI.Xaml;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class UserRecipesControl : UserControl
    {
        #region Dependency Property

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(object), typeof(UserRecipesControl), new PropertyMetadata(null));
        public static readonly DependencyProperty NoItemsImageProperty = DependencyProperty.Register("NoItemsImage", typeof(string), typeof(UserRecipesControl), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty NoItemsTextProperty = DependencyProperty.Register("NoItemsText", typeof(string), typeof(UserRecipesControl), new PropertyMetadata(string.Empty));

        #endregion

        #region CLR Proprety Wrapper

        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public string NoItemsText
        {
            get { return (string)GetValue(NoItemsTextProperty); }
            set { SetValue(NoItemsTextProperty, value); }
        }

        public string NoItemsImage
        {
            get { return (string)GetValue(NoItemsImageProperty); }
            set { SetValue(NoItemsImageProperty, value); }
        }

        #endregion

        #region Life Cycle

        public UserRecipesControl()
        {
            this.InitializeComponent();
            this.root.DataContext = this;
        }

        #endregion

        #region Event

        private void Recipe_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dataContext = sender.GetDataContext();
            
            if(dataContext is UserRecipe)
            {
                var recipe = dataContext as UserRecipe;
                RecipeInfoPage.RecipeInfoPageParams paras = new RecipeInfoPage.RecipeInfoPageParams();
                paras.RecipeId = recipe.Id;

                App.Current.RootFrame.Navigate(typeof(RecipeInfoPage), paras);
            }
            else if (dataContext is UserProduct)
            {
                var product = dataContext as UserProduct;
                SingleProductViewPage.SingleProductViewPageParams paras = new SingleProductViewPage.SingleProductViewPageParams();
                paras.ProductId = product.Id;

                App.Current.RootFrame.Navigate(typeof(SingleProductViewPage), paras);
            }
        }

        #endregion
    }
}
