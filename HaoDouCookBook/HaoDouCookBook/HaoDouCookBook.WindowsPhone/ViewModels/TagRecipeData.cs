using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.ViewModels
{
    public class TagRecipeData : BindableBase
    {
        private string recipeName;

        public string RecipeName
        {
            get { return recipeName; }
            set { SetProperty<string>(ref recipeName, value); }
        }

        private int likeNumber;

        public int LikeNumber
        {
            get { return likeNumber; }
            set { SetProperty<int>(ref likeNumber, value); }
        }

        private int viewNumber;

        public int ViewNumber
        {
            get { return viewNumber; }
            set { SetProperty<int>(ref viewNumber, value); }
        }

        private string foodMaterial;

        public string FoodMaterial
        {
            get { return foodMaterial; }
            set { SetProperty<string>(ref foodMaterial, value); }
        }

        private string previewImageSource;

        public string PreviewImageSource
        {
            get { return previewImageSource; }
            set { SetProperty<string>(ref previewImageSource, value); }
        }

        public TagRecipeData()
        {
            recipeName = string.Empty;
            previewImageSource = string.Empty;
            likeNumber = 0;
            viewNumber = 0;
            foodMaterial = string.Empty;
        }
        
    }
}
