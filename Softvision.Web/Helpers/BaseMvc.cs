using Softvision.BL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Softvision.All.Helpers.Base
{
    public class BaseMVC
    {
        private static UserBL _instance = new UserBL();
        public static UserBL User;

        static UserBL getInstance()
        {
            return Softvision.Helpers.Web.Instance.Session.User();
        }

        public static bool IsLoggedIn()
        {
            User = getInstance();
            if (User == null)
            {
                return false;
            }

            return true;
        }

        public static bool IsAdmin()
        {
            if (!IsLoggedIn())
                return false;

            if (User.UserType == UserTypeBL.Admin)
            {
                return true;
            }
            return false;
        }

        public static UserBL getUser()
        {
            if (!IsLoggedIn())
            {
                return null;
            }

            return User;
        }

        public static int getUserId()
        {
            if (!IsLoggedIn())
            {
                return 0;
            }

            return User.Id;
        }

        public static void setUser(UserBL user)
        {
            Softvision.Helpers.Web.Instance.Session.setUser(user);
        }

        public static void unsetUser()
        {
            Softvision.Helpers.Web.Instance.Session.unsetUser();
        }
    }
}