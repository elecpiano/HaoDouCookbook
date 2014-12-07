using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoDouCookBook.ViewModels
{
    public class UserProfilePageViewModel : BindableBase
    {

        private bool isSignedInUser;

        public bool IsSignedInUser
        {
            get { return isSignedInUser; }
            set { SetProperty<bool>(ref isSignedInUser, value); }
        }

        public UserProfilePageViewModel()
        {
            isSignedInUser = false;
        }
    }
}
