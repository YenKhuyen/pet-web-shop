using pet_web_shop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pet_web_shop.Models.ViewModels
{
    public class ProductDetailViewModels
    {
        public tb_product ProductDetail { get; set; }

        public List<tb_category> CategoryList { get; set; }

        public List<tb_product> TopSaleProduct { get; set; }
    }
}