using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;

namespace Spar.Retail.UI
{
    public class Profile : ProfileBase
    {
        public static Profile Current
        {
            get { return (Profile)(ProfileBase.Create(HttpContext.Current.User.Identity.Name)); }
        }

        public  string ProfileType 
        {
            get { return ((string)(base["ProfileType"])); }
            set { base["ProfileType"] = value; Save(); }
        }
    }
}