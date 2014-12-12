using Shared.Infrastructures;
using Shared.Utility;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace HaoDouCookBook.ViewModels
{
    [DataContract]
    public class PublishRecipePageViewModel : BindableBase
    {
        private int recipeId;

        [DataMember]
        public int RecipeId
        {
            get { return recipeId; }
            set { SetProperty<int>(ref recipeId, value); }
        }

        private string recipeName;

        [DataMember]
        public string RecipeName
        {
            get { return recipeName; }
            set
            {
                ContentChanged = true;
                SetProperty<string>(ref recipeName, value);
            }
        }

        private string intro;

        [DataMember]
        public string Intro
        {
            get { return intro; }
            set { SetProperty<string>(ref intro, value); }
        }

        private string skills;

        [DataMember]
        public string Skills
        {
            get { return skills; }
            set { SetProperty<string>(ref skills, value); }
        }

        public bool ContentChanged { get; set; }

        [DataMember]
        public ObservableCollection<StuffItem> MainStuffs { get; set; }

        [DataMember]
        public ObservableCollection<StuffItem> OtherStuffs { get; set; }

        [DataMember]
        public ObservableCollection<PublishRecipeStep> RecipeSteps { get; set; }

        private bool isInStepOne;

        public bool IsInStepOne
        {
            get { return isInStepOne; }
            set { SetProperty<bool>(ref isInStepOne, value); }
        }

        private string recipeCover;

        [DataMember]
        public string RecipeCover
        {
            get { return recipeCover; }
            set { SetProperty<string>(ref recipeCover, value); }
        }

        public string GetMainStuffsJson()
        {
            if (MainStuffs.Count == 1)
            {
                if(string.IsNullOrEmpty(MainStuffs[0].Name)
                    && string.IsNullOrEmpty(MainStuffs[0].Name))
                {
                    return "[]";
                }
            }

            return JsonSerializer.Serialize(MainStuffs);
        }

        public string GetOtherStuffsJson()
        {
            if (OtherStuffs.Count == 1)
            {
                if(string.IsNullOrEmpty(OtherStuffs[0].Name)
                    && string.IsNullOrEmpty(OtherStuffs[0].Weight))
                {
                    return "[]";
                }
            }
            return JsonSerializer.Serialize(OtherStuffs);
        }

        public PublishRecipePageViewModel()
        {
            recipeName = string.Empty;
            intro = string.Empty;
            skills = string.Empty;
            MainStuffs = new ObservableCollection<StuffItem>();
            MainStuffs.Add(new StuffItem());
            MainStuffs.CollectionChanged += (s, e) => { ContentChanged = true; };
            OtherStuffs = new ObservableCollection<StuffItem>();
            OtherStuffs.Add(new StuffItem());
            OtherStuffs.CollectionChanged += (s, e) => { ContentChanged = true; };
            ContentChanged = false;
            IsInStepOne = true;
            RecipeSteps = new ObservableCollection<PublishRecipeStep>();
            RecipeSteps.Add(new PublishRecipeStep());
        }
    }

    [DataContract]
    public class PublishRecipeStep : BindableBase
    {
        private int stepNumber;

        [DataMember]
        public int StepNumber
        {
            get { return stepNumber; }
            set { SetProperty<int>(ref stepNumber, value); }
        }

        private string stepPhoto;

        [DataMember]
        public string StepPhoto
        {
            get { return stepPhoto; }
            set { SetProperty<string>(ref stepPhoto, value); }
        }

        private string stepIntro;

        [DataMember]
        public string StepIntro
        {
            get { return stepIntro; }
            set { SetProperty<string>(ref stepIntro, value); }
        }

        public PublishRecipeStep()
        {
            stepNumber = 0;
            stepPhoto = string.Empty;
            stepIntro = string.Empty;
        }
    }

}
