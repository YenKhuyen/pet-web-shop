using pet_web_shop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pet_web_shop.Common
{
    public class OrderCommon
    {
        public static string OrderStatus(int status)
        {
            switch (status)
            {
                case Constants.Shipping:
                    return "Đang giao hàng";
                case Constants.Delivered:
                    return "Giao hàng thành công";
                case Constants.Cancelled:
                    return "Đơn hàng đã huỷ";
                default:
                    return "Đơn hàng đã được đặt";
            }
        }
        public static string Image(tb_product product)
        {
            var image_list = product.images_product.OrderBy(x => x.created);
            var image = image_list.Count() > 0 ? image_list.Where(x => x.isDefault).Count() != 0 ? image_list.Where(x => x.isDefault).First().image : image_list.FirstOrDefault().image : "/Content/img/default-product_450.png";
            return image;
        }

        public static string OrderStatusStyle(int status)
        {
            switch (status)
            {
                case Constants.Shipping:
                    return "text-info";
                case Constants.Delivered:
                    return "text-success";
                case Constants.Cancelled:
                    return "text-danger";
                default:
                    return "text-primary";
            }
        }

        public static decimal GetPriceProduct(tb_product product)
        {
            var price = product.isSale ? product.price - product.price * product.sale / 100 : product.price;
            return decimal.Parse(price.ToString());
        }

        //Status
        public class OrderStatusItem
        {
            public string Label { get; set; }
            public int Status { get; set; }
            public string Icon { get; set; }
        }

        public class OrderStatusProgress
        {
            public string Label { get; set; }
            public int Status { get; set; }
        }
    }
}