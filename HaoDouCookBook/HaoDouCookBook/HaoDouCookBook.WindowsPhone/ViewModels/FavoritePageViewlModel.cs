using Shared.Infrastructures;
using System.Collections.ObjectModel;

namespace HaoDouCookBook.ViewModels
{
    #region Recipe

    public class FavoriteRecipesViewModel : BindableBase
    {
        private RecipesAblum myLikeRecipesAlbum;

        public RecipesAblum MyLikeRecipesAlbum
        {
            get { return myLikeRecipesAlbum; }
            set { SetProperty<RecipesAblum>(ref myLikeRecipesAlbum, value); }
        }

        private RecipesAblum myFavoriteRecipesAlbum;

        public RecipesAblum MyFavoriteRecipesAlbum
        {
            get { return myFavoriteRecipesAlbum; }
            set { SetProperty<RecipesAblum>(ref myFavoriteRecipesAlbum, value); }
        }

        public ObservableCollection<RecipesAblum> AlbumsCreatedByMe { get; set; }

        public FavoriteRecipesViewModel()
        {
            myLikeRecipesAlbum = new RecipesAblum();
            myFavoriteRecipesAlbum = new RecipesAblum();
            AlbumsCreatedByMe = new ObservableCollection<RecipesAblum>();
        }
    }

    public class RecipesAblum : BindableBase
    {
        private string cover;

        public string Cover
        {
            get { return cover; }
            set { SetProperty<string>(ref cover, value); }
        }

        private string albumName;

        public string AlbumName
        {
            get { return albumName; }
            set { SetProperty<string>(ref albumName, value); }
        }

        private int recipeNumber;

        public int RecipeNumber
        {
            get { return recipeNumber; }
            set { SetProperty<int>(ref recipeNumber, value); }
        }

        private int albumId;

        public int AlbumId
        {
            get { return albumId; }
            set { SetProperty<int>(ref albumId, value); }
        }

        public RecipesAblum()
        {
            cover = string.Empty;
            albumName = string.Empty;
            recipeNumber = 0;
            albumId = 0;
        }
    }


    #endregion

    #region Album

    public class FavoriteAlbumItem : BindableBase
    {
        private string cover;

        public string Cover
        {
            get { return cover; }
            set { SetProperty<string>(ref cover, value); }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { SetProperty<string>(ref title, value); }
        }

        private string intro;

        public string Intro
        {
            get { return intro; }
            set { SetProperty<string>(ref intro, value); }
        }

        
        private int albumId;

        public int AlbumId
        {
            get { return albumId; }
            set { SetProperty<int>(ref albumId, value); }
        }


        public FavoriteAlbumItem()
        {
            albumId = 0;
            cover = string.Empty;
            title = string.Empty;
            intro = string.Empty;
        }
    }

    #endregion

    #region Topic

    public class FavoriteTopicItem : BindableBase
    {
        private string cover;

        public string Cover
        {
            get { return cover; }
            set { SetProperty<string>(ref cover, value); }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { SetProperty<string>(ref title, value); }
        }

        private string intro;

        public string Intro
        {
            get { return intro; }
            set { SetProperty<string>(ref intro, value); }
        }

        private int topicId;

        public int TopicId
        {
            get { return topicId; }
            set { SetProperty<int>(ref topicId, value); }
        }

        private string url;

        public string Url
        {
            get { return url; }
            set { SetProperty<string>(ref url, value); }
        }


        public FavoriteTopicItem()
        {
            cover = string.Empty;
            title = string.Empty;
            intro = string.Empty;
            topicId = 0;
            url = string.Empty;
        }

    }

    #endregion
}
