using Shared.Infrastructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace HaoDouCookBook.Models
{
    public class UserData : BindableBase
    {

        private string userPhoto;

        public string UserPhoto
        {
            get { return userPhoto; }
            set { SetProperty<string>(ref userPhoto, value); }
        }

        private string uesrId;

        public string UserID 
        {
            get { return uesrId; }
            set { SetProperty<string>(ref uesrId, value); }
        }
        

        public UserData()
        {

        }
    }
}
