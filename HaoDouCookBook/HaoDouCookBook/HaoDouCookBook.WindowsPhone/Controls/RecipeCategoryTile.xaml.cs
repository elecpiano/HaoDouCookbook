using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace HaoDouCookBook.Controls
{
    public sealed partial class RecipeCategoryTile : UserControl
    {
        #region Dependency Property

        public static readonly DependencyProperty TileImageProperty = DependencyProperty.Register("TileImage", typeof(string), typeof(RecipeCategoryTile), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty MarkTextProperty = DependencyProperty.Register("MarkText", typeof(string), typeof(RecipeCategoryTile), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty TitleIconProperty = DependencyProperty.Register("TitleIcon", typeof(string), typeof(RecipeCategoryTile), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(RecipeCategoryTile), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(RecipeCategoryTile), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty RecipeNameProperty = DependencyProperty.Register("RecipeName", typeof(string), typeof(RecipeCategoryTile), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty AuthorRecipeCommentProperty = DependencyProperty.Register("AuthorRecipeComment", typeof(string), typeof(RecipeCategoryTile), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty RecipeDescriptionOnImageProperty = DependencyProperty.Register("RecipeDescriptionOnImage", typeof(string), typeof(RecipeCategoryTile), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty AuthorProperty = DependencyProperty.Register("Author", typeof(string), typeof(RecipeCategoryTile), new PropertyMetadata(string.Empty));

        #endregion

        #region CLR Property Wrapper

        public string TileImage
        {
            get { return (string)GetValue(TileImageProperty); }
            set { SetValue(TileImageProperty, value); }
        }

        public string MarkText
        {
            get { return (string)GetValue(MarkTextProperty); }
            set { SetValue(MarkTextProperty, value); }
        }

        public string TitleIcon
        {
            get { return (string)GetValue(TitleIconProperty); }
            set { SetValue(TitleIconProperty, value); }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public string RecipeName
        {
            get { return (string)GetValue(RecipeNameProperty); }
            set { SetValue(RecipeNameProperty, value); }
        }

        public string AuthorRecipeComment
        {
            get { return (string)GetValue(AuthorRecipeCommentProperty); }
            set { SetValue(AuthorRecipeCommentProperty, value); }
        }

        public string RecipeDescriptionOnImage
        {
            get { return (string)GetValue(RecipeDescriptionOnImageProperty); }
            set { SetValue(RecipeDescriptionOnImageProperty, value); }
        }

        public string Author
        {
            get { return (string)GetValue(AuthorProperty); }
            set { SetValue(AuthorProperty, value); }
        }




        #endregion

        #region Life Cycle

        public RecipeCategoryTile()
        {
            this.InitializeComponent();
            this.root.DataContext = this;
        }

        #endregion
    }
}
