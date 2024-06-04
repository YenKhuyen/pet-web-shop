using Microsoft.Owin;
using Owin;

namespace pet_web_shop
{
    public static class Constants
    {
        //User role
        public const int RoleUser = 1;
        public const int RoleAdmin = 2;
        public const int RoleOwner = 3;

        //User status
        public const int ActiveUser = 1;
        public const int DeactivatedUser = 0;

        //Cart status
        public const int OnCart = 0;
        public const int CartOrdered = 1;

        //Order status
        public const int Ordered = 0;
        public const int Shipping = 1;
        public const int Delivered = 2;
        public const int Cancelled = 3;

        //Session user name
        public static string USER_SESSION = "userSession";
    }
}