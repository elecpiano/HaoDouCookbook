using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.ViewModels
{
    public class PublishRecipePageViewModel : BindableBase
    {
        private string recipeName;

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

        public string Intro
        {
            get { return intro; }
            set { SetProperty<string>(ref intro, value); }
        }

        private string skills;

        public string Skills
        {
            get { return skills; }
            set { SetProperty<string>(ref skills, value); }
        }

        public bool ContentChanged { get; set; }

        public ObservableCollection<StuffItem> MainStuffs { get; set; }

        public ObservableCollection<StuffItem> OtherStuffs { get; set; }

        public ObservableCollection<PublishRecipeStep> RecipeSteps { get; set; }

        private bool isInStepOne;

        public bool IsInStepOne
        {
            get { return isInStepOne; }
            set { SetProperty<bool>(ref isInStepOne, value); }
        }

        private string recipeCover;

        public string RecipeCover
        {
            get { return recipeCover; }
            set { SetProperty<string>(ref recipeCover, value); }
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

    public class PublishRecipeStep : BindableBase
    {
        private int stepNumber;

        public int StepNumber
        {
            get { return stepNumber; }
            set { SetProperty<int>(ref stepNumber, value); }
        }

        private string stepPhoto;

        public string StepPhoto
        {
            get { return stepPhoto; }
            set { SetProperty<string>(ref stepPhoto, value); }
        }

        private string stepIntro;

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
