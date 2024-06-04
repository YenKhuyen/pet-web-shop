using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pet_web_shop.Common
{
    public class GetRoleName
    {
        public static string RoleName(int role)
        {
            switch (role)
            {
                case Constants.RoleUser:
                    return "User";
                case Constants.RoleAdmin:
                    return "Admin";
                default:
                    return "Owner";
            }
        }
    }
}