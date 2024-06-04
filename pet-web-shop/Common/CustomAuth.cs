using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pet_web_shop.Common
{
    public class CustomAuth
    {

        public static Boolean CheckAuth(HttpSessionStateBase Session)
        {
            var session = Session[Constants.USER_SESSION] as UserLogin;
            if (session == null)
            {
                return false;
            }

            if (DateTime.Parse(session.valid_until) < DateTime.Now)
            {
                return false;
            }

            return true;
        }
    }
}