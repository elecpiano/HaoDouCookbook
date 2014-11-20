using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace HaoDouCookBook.ViewModels 
{
    public class Meal : BindableBase
    {

        private int number;

        public int Number
        {
            get { return number; }
            set { SetProperty<int>(ref number, value); }
        }

        private string mealImageSource;

        public string MealImageSource
        {
            get { return mealImageSource; }
            set { SetProperty<string>(ref mealImageSource, value); }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { SetProperty<string>(ref title, value); }
        }


        public Meal()
        {
            Number = 0;
            mealImageSource = string.Empty;
            title = string.Empty;
        }
    }
}
