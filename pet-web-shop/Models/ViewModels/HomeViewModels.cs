using pet_web_shop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pet_web_shop.Models.ViewModels
{
    public class HomeViewModels
    {
        public List<tb_category> ListCate { get; set; }
    }
}