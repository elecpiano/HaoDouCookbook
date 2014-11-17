using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace HaoDouCookBook.Models 
{
    public class Meal : BindableBase
    {
        private string date;

        public string Date
        {
            get { return date; }
            set { SetProperty<string>(ref date, value); }
        }

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

        private string name;

        public string Name
        {
            get { return name; }
            set { SetProperty<string>(ref name, value); }
        }


        public Meal()
        {
            DateTime now = DateTime.Now;
            date = String.Format("{0}月{1}日", now.Month, now.Day);
            Number = 0;
            mealImageSource = string.Empty;
        }
    }
}
