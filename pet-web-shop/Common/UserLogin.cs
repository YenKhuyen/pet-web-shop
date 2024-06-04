using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pet_web_shop.Common
{
    public class UserLogin
    {
        public int id { set; get; }
        public string user_name { set; get; }
        public string valid_until { set; get; }
    }
}