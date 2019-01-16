using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMS_Blog.Models
{
    public class SessionModel
    {
        public string UserName
        {
            get;
            set;
        }
        public string User_Pwd
        {
            get;
            set;
        }
        public string Session_Val
        {
            get;
            set;
        }
    }
}