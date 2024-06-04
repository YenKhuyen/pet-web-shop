using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace pet_web_shop.Models.ViewModels
{
    public class ChangePassViewModels
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu hiện tại!")]
        public string password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu mới!")]
        public string new_password { get; set; }

        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu mới!")]
        public string re_new_password { get; set; }
    }
}