using pet_web_shop.Common;
using pet_web_shop.Models.DAO;
using pet_web_shop.Models.EF;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pet_web_shop.Controllers
{
    public class CartController : Controller
    {
        CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
        public ActionResult Auth()
        {
            var check = CustomAuth.CheckAuth(Session);
            if (!check)
            {
                return Redirect("~/dang-nhap");
            }

            var session = Session[Constants.USER_SESSION] as UserLogin;
            var dao = new User_DAO();
            var user = dao.GetItemByID(session.id);
            if (user.role != Constants.RoleUser)
            {
                return Redirect("~/home-admin");
            }

            return null;
        }

        // GET: Cart
        public ActionResult Index()
        {
            var authResult = Auth();
            if (authResult != null)
            {
                return authResult;
            }
            var dao = new Cart_DAO();
            var user_id = (Session[Constants.USER_SESSION] as UserLogin).id;

            var list_cart = dao.GetCart(user_id);


            return View(list_cart);
        }

        [HttpGet]
        public ActionResult CountCart()
        {
            var dao = new Cart_DAO();
            var session = Session[Constants.USER_SESSION] as UserLogin;

            if (session != null)
            {
                var user_id = session.id;
                var count = dao.GetCount(user_id);
                return Json(new { success = true, count = count }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, count = 0 }, JsonRequestBehavior.AllowGet);
        }

        // POST: Cart/Create
        [HttpPost]
        public ActionResult Create(int product_id, int quantity)
        {
            try
            {
                var authResult = Auth();
                if (authResult != null)
                {
                    return Json(new { success = false, url = "/dang-nhap" }, JsonRequestBehavior.AllowGet);
                }
                var dao = new Cart_DAO();
                var user_id = (Session[Constants.USER_SESSION] as UserLogin).id;
                var cart = dao.Add(product_id, user_id, quantity);
                if (cart)
                {
                    return Json(new { success = true, msg = "Thêm vào giỏ hàng thành công!" });
                }


                return Json(new { success = false, msg = "Đã xảy ra lỗi, vui lòng thử lại sau!" });
            }
            catch
            {
                return Json(new { success = false, msg = "Đã xảy ra lỗi, vui lòng thử lại sau!" });
            }
        }

        // POST: Cart/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, int quantity)
        {
            try
            {
                var authResult = Auth();
                if (authResult != null)
                {
                    return Json(new { success = false, url = "/dang-nhap" });
                }
                var dao = new Cart_DAO();

                var cart = dao.UpdateQuantity(id, quantity);
                var user_id = (Session[Constants.USER_SESSION] as UserLogin).id;

                if (cart)
                {
                    var list_cart = dao.GetCart(user_id);
                    var total = double.Parse(list_cart.Sum(x => x.price * x.quantity).ToString()).ToString("#,###", cul.NumberFormat);
                    return Json(new { success = true, msg = "Cập nhật giỏ hàng thành công!", total = total });
                }

                var list_ = dao.GetCart(user_id);
                var total_ = double.Parse(list_.Sum(x => x.price * x.quantity).ToString()).ToString("#,###", cul.NumberFormat);
                return Json(new { success = false, msg = "Đã xảy ra lỗi, vui lòng thử lại sau!", total = total_ });
            }
            catch
            {
                var dao = new Cart_DAO();
                var user_id = (Session[Constants.USER_SESSION] as UserLogin).id;
                var list_ = dao.GetCart(user_id);
                var total_ = double.Parse(list_.Sum(x => x.price * x.quantity).ToString()).ToString("#,###", cul.NumberFormat);
                return Json(new { success = false, msg = "Đã xảy ra lỗi, vui lòng thử lại sau!", total = total_ });
            }
        }

        // POST: Cart/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var authResult = Auth();
                if (authResult != null)
                {
                    return authResult;
                }
                var dao = new Cart_DAO();
                var cart = dao.Remove(id);

                if (cart)
                {
                    var user_id = (Session[Constants.USER_SESSION] as UserLogin).id;

                    var list_cart = dao.GetCart(user_id);
                    var total = double.Parse(list_cart.Sum(x => x.price * x.quantity).ToString()).ToString("#,###", cul.NumberFormat);

                    List<dynamic> arr = new List<dynamic>();
                    foreach (tb_cart cart_item in list_cart)
                    {
                        var image_list = cart_item.product.images_product.OrderBy(x => x.created);
                        var image = image_list.Count() > 0 ? image_list.Where(x => x.isDefault).Count() != 0 ? image_list.Where(x => x.isDefault).First().image : image_list.FirstOrDefault().image : "/Content/img/default-product_450.png";
                        var price = cart_item.price;
                        string price_string = double.Parse(price.ToString()).ToString("#,###", cul.NumberFormat);
                        arr.Add(new
                        {
                            id = cart_item.id,
                            price = price_string,
                            image = image,
                            title = cart_item.product.title,
                            product_quantity = cart_item.product.quantity,
                            quantity = cart_item.quantity,
                        });
                    }
                    return Json(new { success = true, msg = "Xoá sản phẩm thành công!", list_cart = arr.ToArray(), total = total });
                }

                return Json(new { success = false, msg = "Đã xảy ra lỗi, vui lòng thử lại sau!" });
            }
            catch
            {
                return Json(new { success = false, msg = "Đã xảy ra lỗi, vui lòng thử lại sau!" });
            }
        }

        [HttpPost]
        public ActionResult RemoveAll()
        {
            try
            {
                var authResult = Auth();
                if (authResult != null)
                {
                    return authResult;
                }
                var dao = new Cart_DAO();

                var user_id = (Session[Constants.USER_SESSION] as UserLogin).id;
                var removed = dao.RemoveAll(user_id);
                if (!removed)
                {
                    ModelState.AddModelError("", "Xoá giỏ hàng thất bại, vui lòng thử lại sau!");
                    var list_cart = dao.GetCart(user_id);
                    return RedirectToAction("Index");
                }

                return Redirect("~/gio-hang");
            }
            catch
            {
                var authResult = Auth();
                if (authResult != null)
                {
                    return authResult;
                }
                var dao = new Cart_DAO();
                var user_id = (Session[Constants.USER_SESSION] as UserLogin).id;

                var list_cart = dao.GetCart(user_id);

                ModelState.AddModelError("", "Đã xảy ra lỗi, vui lòng thử lại sau!");
                return RedirectToAction("Index");
            }
        }
    }
}
