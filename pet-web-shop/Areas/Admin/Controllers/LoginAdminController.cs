using pet_web_shop.Common;
using pet_web_shop.Models.DAO;
using pet_web_shop.Models.EF;
using pet_web_shop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pet_web_shop.Areas.Admin.Controllers
{
    public class LoginAdminController : Controller
    {
        // GET: Admin/LoginAdmin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginViewModels model)
        {
            if (ModelState.IsValid)
            {
                var dao = new User_DAO();
                var result = dao.Login(model.user_name, Encryptor.MD5Hash(model.password));

                switch (result)
                {
                    case -2:
                        ModelState.AddModelError("", "Tên đăng nhập không chính xác!");
                        break;
                    case -1:
                        ModelState.AddModelError("", "Mật khẩu không chính xác!");
                        break;
                    case 0:
                        ModelState.AddModelError("", "Tài khoản của bạn đã bị vô hiệu hoá!");
                        break;
                    default:
                        var session = new UserLogin();
                        session.id = Convert.ToInt32(result.id);
                        session.user_name = Convert.ToString(result.user_name);
                        session.valid_until = DateTime.Now.AddDays(1).ToString();
                        Session.Add(Constants.USER_SESSION, session);

                        if ((result as tb_account).role == Constants.RoleUser)
                        {
                            return Redirect("~/");
                        }

                        return Redirect("~/home-admin");
                }
            }
            return View("Login");
        }

        public ActionResult Logout()
        {
            Session[Constants.USER_SESSION] = null;
            return Redirect("~/");
        }


        [HttpGet]
        public ActionResult CheckAuth()
        {
            var session = Session[Constants.USER_SESSION] as UserLogin;
            if (session == null)
                return Json(new { isLoggedIn = false }, JsonRequestBehavior.AllowGet);
            var dao = new User_DAO();
            var user = dao.GetItemByID(session.id);
            var avatar = user.avatar != null ? "data:image/png;base64," + Convert.ToBase64String(user.avatar) : "/Content/img/Default_avt.png";

            return Json(new { isLoggedIn = true, id = session.id, userName = session.user_name, avatar = avatar }, JsonRequestBehavior.AllowGet);
        }
    }
}