using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace pet_web_shop.Models.ViewModels
{
    public class LoginViewModels
    {
        [Required(ErrorMessage = "Cần nhập tên đăng nhập để tiếp tục!")]
        public string user_name { get; set; }

        [Required(ErrorMessage = "Cần nhập mật khẩu để tiếp tục!")]
        public string password { get; set; }
    }
}