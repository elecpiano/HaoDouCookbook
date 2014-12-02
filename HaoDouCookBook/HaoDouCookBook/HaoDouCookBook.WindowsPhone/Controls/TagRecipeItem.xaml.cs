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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class TagRecipeItem : UserControl
    {

        #region Dependency Property


        public static readonly DependencyProperty LikeNumberProperty = DependencyProperty.Register("LikeNumber", typeof(int), typeof(TagRecipeItem), new PropertyMetadata(0));
        public static readonly DependencyProperty ViewNumberProperty = DependencyProperty.Register("ViewNumber", typeof(int), typeof(TagRecipeItem), new PropertyMetadata(0));
        public static readonly DependencyProperty FoodStuffProperty = DependencyProperty.Register("FoodStuff", typeof(string), typeof(TagRecipeItem), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty PreviewImageSourceProperty = DependencyProperty.Register("PreviewImageSource", typeof(string), typeof(TagRecipeItem), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty RecipeNameProperty = DependencyProperty.Register("RecipeName", typeof(string), typeof(TagRecipeItem), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty CardTextProperty = DependencyProperty.Register("CardText", typeof(string), typeof(TagRecipeItem), new PropertyMetadata(string.Empty));

        #endregion

        #region CLR Proprety Wrapper

        public string CardText
        {
            get { return (string)GetValue(CardTextProperty); }
            set { SetValue(CardTextProperty, value); }
        }

        public int LikeNumber
        {
            get { return (int)GetValue(LikeNumberProperty); }
            set { SetValue(LikeNumberProperty, value); }
        }


        public int ViewNumber
        {
            get { return (int)GetValue(ViewNumberProperty); }
            set { SetValue(ViewNumberProperty, value); }
        }

        public string FoodStuff
        {
            get { return (string)GetValue(FoodStuffProperty); }
            set { SetValue(FoodStuffProperty, value); }
        }

        public string PreviewImageSource
        {
            get { return (string)GetValue(PreviewImageSourceProperty); }
            set { SetValue(PreviewImageSourceProperty, value); }
        }

        public string RecipeName
        {
            get { return (string)GetValue(RecipeNameProperty); }
            set { SetValue(RecipeNameProperty, value); }
        }

	    #endregion


        #region Life Cycle

        public TagRecipeItem()
        {
            this.InitializeComponent();
            this.root.DataContext = this;
        }

	    #endregion
    }
}
