using HaoDouCookBook.Pages;
using HaoDouCookBook.ViewModels;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Shared.Utility;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class Cate : UserControl
    {
        #region Dependency Property

        public static readonly DependencyProperty CateTitleProperty = DependencyProperty.Register("CateTitle", typeof(string), typeof(Cate), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty RecipesProperty = DependencyProperty.Register("Recipes", typeof(ObservableCollection<DishTileData>), typeof(Cate), new PropertyMetadata(null));
        
        #endregion

        #region CLR Property Wrapper

        public string CateTitle
        {
            get { return (string)GetValue(CateTitleProperty); }
            set { SetValue(CateTitleProperty, value); }
        }

        public ObservableCollection<DishTileData> Recipes
        {
            get { return (ObservableCollection<DishTileData>)GetValue(RecipesProperty); }
            set { SetValue(RecipesProperty, value); }
        }

        #endregion

        public Cate()
        {
            this.InitializeComponent();
            this.root.DataContext = this;
            
        }

        #region Public Methods

        public void AdjustLayout(int index)
        {
            if (index % 2 == 0)
            {
                cateItem10.Height = 200;
                cateItem11.Height = 240;
                cateItem20.Height = 240;
                cateItem21.Height = 200;
            }
            else
            {
                cateItem10.Height = 245;
                cateItem11.Height = 195;
                cateItem20.Height = 195;
                cateItem21.Height = 245;
            }
        }

        #endregion

        #region Event

        private void cateItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dish = sender.GetDataContext<DishTileData>();
            ProductPage.ProductPageParams paras = new ProductPage.ProductPageParams();
            paras.ProductId = dish.ProductId;
            paras.TopicId = dish.Id;
            paras.Type = 1;

            App.CurrentInstance.RootFrame.Navigate(typeof(ProductPage), paras);
        }

        #endregion
    }
}
