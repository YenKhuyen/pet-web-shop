using pet_web_shop.Common;
using pet_web_shop.Models.DAO;
using pet_web_shop.Models.EF;
using pet_web_shop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pet_web_shop.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Auth()
        {
            var check = CustomAuth.CheckAuth(Session);
            if (!check)
            {
                return Redirect("~/dang-nhap");
            }

            //var session = Session[Constants.USER_SESSION] as UserLogin;
            //var dao = new User_DAO();
            //var user = dao.GetItemByID(session.id);
            //if (user.role != Constants.RoleUser)
            //{
            //    return Redirect("~/home-admin");
            //}

            return null;
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit()
        {
            try
            {
                var authResult = Auth();
                if (authResult != null)
                {
                    return authResult;
                }

                var dao = new User_DAO();
                var user_id = (Session[Constants.USER_SESSION] as UserLogin).id;
                var user = dao.GetItemByID(user_id);

                if (user == null)
                {
                    Redirect("~/dang-nhap");
                }


                return View(user);
            }
            catch
            {
                return Redirect("~/");
            }
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tb_account user, string current_pass)
        {
            try
            {
                var authResult = Auth();
                if (authResult != null)
                {
                    return authResult;
                }

                if (ModelState.IsValid)
                {
                    var session = Session[Constants.USER_SESSION] as UserLogin;
                    var dao = new User_DAO();

                    var current_user = dao.GetItemByID(session.id);
                    var owner = dao.GetOwner();
                    if (owner.id == user.id && owner != current_user)
                    {
                        ModelState.AddModelError("", "Bạn không có quyền cập nhật tài khoản này!");
                        return View(user);
                    }

                    dao.Detached(owner);
                    dao.Detached(current_user);

                    if (user != null)
                    {
                        var updated = dao.Update(user, current_pass);
                        if (updated != null)
                        {
                            ModelState.AddModelError("", "Cập nhật thông tin thành công!");
                            return View(user);
                        }
                        else
                        {
                            ModelState.AddModelError("", "Cập nhật thôn tin không thành công! Vui lòng thử lại sau!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Không có quyền cập nhật thông tin người dùng!");
                    }
                }

                return View(user);
            }
            catch
            {
                ModelState.AddModelError("", "Cập nhật thôn tin không thành công! Vui lòng thử lại sau!");
                return View(user);
            }
        }

        [HttpGet]
        public ActionResult ChangePass()
        {
            var authResult = Auth();
            if (authResult != null)
            {
                return authResult;
            }

            var session = Session[Constants.USER_SESSION] as UserLogin;

            var id = session.id;

            ChangePassViewModels pass = new ChangePassViewModels
            {
                id = id,
                password = "",
                new_password = "",
                re_new_password = ""
            };
            return View(pass);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePass(ChangePassViewModels pass)
        {
            var authResult = Auth();
            if (authResult != null)
            {
                return authResult;
            }

            if (ModelState.IsValid)
            {
                var session = Session[Constants.USER_SESSION] as UserLogin;
                var dao = new User_DAO();

                var id = session.id;

                if (id != pass.id)
                {
                    return Redirect("~/dang-nhap");
                }

                var user = dao.GetItemByID(id);

                if (Encryptor.MD5Hash(pass.password) != user.password)
                {
                    ModelState.AddModelError("password", "Mật khẩu không chính xác!");
                    return View(pass);
                }

                if (pass.new_password != pass.re_new_password)
                {
                    ModelState.AddModelError("re_new_password", "Mật khẩu mới không trùng khớp!");
                    return View(pass);
                }

                var updated = dao.ChangePass(pass.new_password, user);

                if (!updated)
                {
                    ModelState.AddModelError("", "Cập nhật mật khẩu thất bại! Vui lòng thử lại sau!");
                    return View(pass);
                }

                ModelState.AddModelError("", "Cập nhật mật khẩu thành công!");
                return View(pass);
            }
            return View(pass);
        }


        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
