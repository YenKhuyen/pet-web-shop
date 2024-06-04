using System.Web.Mvc;

namespace pet_web_shop.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: "AdminLogin",
                url: "dang-nhap",
                defaults: new { controller = "LoginAdmin", action = "Login" }
            );

            context.MapRoute(
                name: "AdminHome",
                url: "home-admin",
                defaults: new { controller = "HomeAdmin", action = "Index" }
            );

            context.MapRoute(
               name: "ProductAdmin",
               url: "admin/quan-ly-san-pham",
               defaults: new { controller = "ProductManagement", action = "Index" }
            );

            context.MapRoute(
              name: "CreateProductAdmin",
              url: "admin/tao-san-pham",
              defaults: new { controller = "ProductManagement", action = "Create" }
            );

            context.MapRoute(
              name: "ProductAdminDetail",
              url: "admin/quan-ly-san-pham/{id}",
              defaults: new { controller = "ProductManagement", action = "Edit", id = UrlParameter.Optional }
            );

            context.MapRoute(
               name: "CategoryAdmin",
               url: "admin/quan-ly-danh-muc",
               defaults: new { controller = "CategoryManagement", action = "Index" }
            );

            context.MapRoute(
               name: "CreateCategoryAdmin",
               url: "admin/tao-danh-muc",
               defaults: new { controller = "CategoryManagement", action = "Create" }
            );

            context.MapRoute(
               name: "CategoryAdminDetail",
               url: "admin/quan-ly-danh-muc/{id}",
               defaults: new { controller = "CategoryManagement", action = "Edit", id = UrlParameter.Optional }
            );

            context.MapRoute(
               name: "UserAdmin",
               url: "admin/quan-ly-tai-khoan",
               defaults: new { controller = "UserManagement", action = "Index" }
            );

            context.MapRoute(
               name: "CreateUserAdmin",
               url: "admin/tao-tai-khoan",
               defaults: new { controller = "UserManagement", action = "create" }
            );

            context.MapRoute(
               name: "UserAdminDetail",
               url: "admin/quan-ly-tai-khoan/{id}",
               defaults: new { controller = "UserManagement", action = "Edit", id = UrlParameter.Optional }
            );

            context.MapRoute(
               name: "PostAdmin",
               url: "admin/quan-ly-bai-viet",
               defaults: new { controller = "PostManagement", action = "Index" }
            );

            context.MapRoute(
               name: "CreatePostAdmin",
               url: "admin/tao-bai-viet",
               defaults: new { controller = "PostManagement", action = "create" }
            );

            context.MapRoute(
               name: "PostAdminDetail",
               url: "admin/quan-ly-bai-viet/{id}",
               defaults: new { controller = "PostManagement", action = "Edit", id = UrlParameter.Optional }
            );
            
            context.MapRoute(
               name: "PostAdminComment",
               url: "admin/quan-ly-bai-viet/{id}/binh-luan",
               defaults: new { controller = "PostManagement", action = "Details", id = UrlParameter.Optional }
            );

            context.MapRoute(
              name: "OrderAdmin",
              url: "admin/quan-ly-don-hang",
              defaults: new { controller = "OrderManagement", action = "Index" }
            );

            context.MapRoute(
              name: "OrderAdminDetail",
              url: "admin/quan-ly-don-hang/{id}",
              defaults: new { controller = "OrderManagement", action = "Edit" }
            );

            context.MapRoute(
              name: "AboutAdmin",
              url: "admin/gioi-thieu",
              defaults: new { controller = "AboutManagement", action = "Index" }
            );
            
            context.MapRoute(
              name: "DetailAboutAdmin",
              url: "admin/trang-gioi-thieu",
              defaults: new { controller = "AboutManagement", action = "Edit" }
            );
           
            context.MapRoute(
              name: "Logout",
              url: "logout",
              defaults: new { controller = "LoginAdmin", action = "Logout" }
            );

            context.MapRoute(
             name: "Register",
             url: "dang-ky-tai-khoan",
             defaults: new { controller = "UserManagement", action = "Register" }
           );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}