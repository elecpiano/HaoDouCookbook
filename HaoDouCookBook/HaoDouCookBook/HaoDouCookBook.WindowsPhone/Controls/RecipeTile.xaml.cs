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
    public sealed partial class RecipeTile : UserControl
    {
        #region Denpendency Property

        public static readonly DependencyProperty RecommendationProperty = DependencyProperty.Register("Recommendation", typeof(string), typeof(RecipeTile), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty TagsTextProperty = DependencyProperty.Register("TagsText", typeof(string), typeof(RecipeTile), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty RecipeNameProperty = DependencyProperty.Register("RecipeName", typeof(string), typeof(RecipeTile), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty AuthorProperty = DependencyProperty.Register("Author", typeof(string), typeof(RecipeTile), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty SupportNumberProperty = DependencyProperty.Register("SupportNumber", typeof(string), typeof(RecipeTile), new PropertyMetadata("0"));
        public static readonly DependencyProperty RecipeImageProperty = DependencyProperty.Register("RecipeImage", typeof(string), typeof(RecipeTile), new PropertyMetadata(string.Empty));

        #endregion

        #region CLR Propery Wrapper

        public string TagsText
        {
            get { return (string)GetValue(TagsTextProperty); }
            set { SetValue(TagsTextProperty, value); }
        }


        public string RecipeName
        {
            get { return (string)GetValue(RecipeNameProperty); }
            set { SetValue(RecipeNameProperty, value); }
        }

        public string Author
        {
            get { return (string)GetValue(AuthorProperty); }
            set { SetValue(AuthorProperty, value); }
        }

        public string SupportNumber
        {
            get { return (string)GetValue(SupportNumberProperty); }
            set { SetValue(SupportNumberProperty, value); }
        }

        public string Recommendation
        {
            get { return (string)GetValue(RecommendationProperty); }
            set { SetValue(RecommendationProperty, value); }
        }

        public string RecipeImage
        {
            get { return (string)GetValue(RecipeImageProperty); }
            set { SetValue(RecipeImageProperty, value); }
        }

        #endregion

        #region Life Cyle

        public RecipeTile()
        {
            this.InitializeComponent();
            this.root.DataContext = this;
        }

        #endregion
    }
}
