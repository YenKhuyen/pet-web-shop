using PagedList;
using pet_web_shop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pet_web_shop.Models.ViewModels
{
    public class CategoriesViewModels
    {
        public tb_category Category { get; set; }
        public IPagedList<tb_product> ListProduct { get; set; }
    }
}