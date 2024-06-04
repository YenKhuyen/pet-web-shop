using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace pet_web_shop.Common
{
    public class FormatNumber
    {
        public static string Format(decimal number)
        {
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            return double.Parse(number.ToString()).ToString("#,###", cul.NumberFormat);
        }
    }
}