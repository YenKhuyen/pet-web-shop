using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace pet_web_shop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                 name: "CategoriesProduct",
                 url: "danh-muc/{id}/san-pham",
                 defaults: new { Controller = "Categories", action = "CategoriesProduct", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                 name: "ProductDetail",
                 url: "chi-tiet-san-pham/{id}",
                 defaults: new { Controller = "Product", action = "Details", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Cart",
                url: "gio-hang",
                defaults: new { Controller = "Cart", action = "index" }
            );

            routes.MapRoute(
               name: "CartRemoveAll",
               url: "xoa-gio-hang",
               defaults: new { Controller = "Cart", action = "RemoveAll" }
            );

            routes.MapRoute(
                name: "Orders",
                url: "don-mua",
                defaults: new { Controller = "Order", action = "index" }
            );

            routes.MapRoute(
                name: "OrdersSuccess",
                url: "dat-hang-thanh-cong",
                defaults: new { Controller = "Order", action = "Success" }
            );

            routes.MapRoute(
                name: "CreateOrder",
                url: "dat-hang",
                defaults: new { Controller = "Order", action = "Create" }
            );

            routes.MapRoute(
               name: "Products",
               url: "san-pham",
               defaults: new { Controller = "Product", action = "Index" }
            );

            routes.MapRoute(
               name: "About",
               url: "gioi-thieu",
               defaults: new { Controller = "About", action = "Index" }
            );

            routes.MapRoute(
              name: "Post",
              url: "bai-viet",
              defaults: new { Controller = "Post", action = "Index" }
            );

            routes.MapRoute(
              name: "PostDetail",
              url: "bai-viet/{id}",
              defaults: new { Controller = "Post", action = "Details", id = UrlParameter.Optional }
            );
            
            routes.MapRoute(
              name: "UserUpdate",
              url: "thong-tin-ca-nhan",
              defaults: new { Controller = "User", action = "Edit"}
            );

             routes.MapRoute(
              name: "ChangePass",
              url: "doi-mat-khau",
              defaults: new { Controller = "User", action = "ChangePass"}
            );

            routes.MapRoute(
                name: "404",
                url: "404",
                defaults: new { Controller = "Error", action = "PageNotFound" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
