using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace HaoDouCookBook.ViewModels
{
    public class CookMaster : BindableBase
    {
        private string cookMasterImageSource;

        public string CookMasterImageSource
        {
            get { return cookMasterImageSource; }
            set { SetProperty<string>(ref cookMasterImageSource, value); }
        }

        public CookMaster()
        {
            cookMasterImageSource = string.Empty;
        }
    }
}
