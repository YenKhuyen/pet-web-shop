using pet_web_shop.Models.DAO;
using pet_web_shop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using pet_web_shop.Common;

namespace pet_web_shop.Areas.Admin.Controllers
{
    public class UserManagementController : Controller
    {
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
            if (user.role == Constants.RoleUser)
            {
                return Redirect("~/");
            }

            return null;
        }

        // GET: Admin/User
        public ActionResult Index(string search, string currentFilter, int? page)
        {
            var authResult = Auth();
            if (authResult != null)
            {
                return authResult;
            }

            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }

            ViewBag.CurrentFilter = search;

            var dao = new User_DAO();
            var userList = dao.GetList(search);

            int pageNumber = (page ?? 1);
            int pageSize = 10;

            return View(userList.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            var authResult = Auth();
            if (authResult != null)
            {
                return authResult;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Create(tb_account acc)
        {
            var authResult = Auth();
            if (authResult != null)
            {
                return authResult;
            }

            if (ModelState.IsValid)
            {
                if (acc.user_name.Contains(" "))
                {
                    ModelState.AddModelError("user_name", "Tên đăng nhập không hợp lệ, không được chứa khoảng trắng!");
                    return View("Register");
                }

                var dao = new User_DAO();

                var checkName = dao.GetItemByNameAndEmail(acc.user_name, null);
                var checkMail = dao.GetItemByNameAndEmail(null, acc.email);

                if (checkMail == null & checkName == null)
                {
                    var ua = dao.Add(acc);
                    if (ua != null)
                    {
                        return RedirectToAction("index", "usermanagement");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm tài khoản không thành công!");
                    }
                }
                else
                {
                    if (checkName != null)
                    {
                        ModelState.AddModelError("", "Tên người dùng đã tồn tại");
                    }
                    if (checkMail != null)
                    {
                        ModelState.AddModelError("", "Email đã được dăng ký");
                    }

                }
            }
            return View("Create");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var authResult = Auth();
            if (authResult != null)
            {
                return authResult;
            }

            if (id != null)
            {
                var dao = new User_DAO();

                var user = dao.GetItemByID(id ?? 0);
                if (user != null)
                {
                    ViewBag.CurrPass = user.password;
                    return View(user);
                }
            }

            return RedirectToAction("index", "usermanagement");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tb_account acc, string current_pass)
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
                if (owner.id == acc.id && owner != current_user)
                {
                    ModelState.AddModelError("", "Bạn không có quyền cập nhật tài khoản này!");
                    return View(acc);
                }

                dao.Detached(owner);
                dao.Detached(current_user);

                if (acc != null)
                {
                    var updated = dao.Update(acc, current_pass);
                    if (updated != null)
                        return RedirectToAction("index", "usermanagement");
                    else
                    {
                        ModelState.AddModelError("", "Cập nhật tài khoản thất bại, vui lòng thử lại sau!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Bạn không có quyền cập nhật tài khoản này!");
                }
            }

            return View(acc);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(tb_account acc)
        {
            if (ModelState.IsValid)
            {
                if (acc.user_name.Contains(" "))
                {
                    ModelState.AddModelError("user_name", "Tên đăng nhập không hợp lệ, không được chứa khoảng trắng!");
                    return View("Register");
                }
                
                var dao = new User_DAO();

                var checkName = dao.GetItemByNameAndEmail(acc.user_name, null);
                var checkMail = dao.GetItemByNameAndEmail(null, acc.email);

                if (checkMail == null & checkName == null)
                {
                    acc.role = Constants.RoleUser;
                    acc.status = Constants.ActiveUser;
                    var ua = dao.Add(acc);
                    if (ua != null)
                    {
                        return Redirect("~/dang-nhap");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm tài khoản không thành công!");
                    }
                }
                else
                {
                    if (checkName != null)
                    {
                        ModelState.AddModelError("", "Tên người dùng đã tồn tại");
                    }
                    if (checkMail != null)
                    {
                        ModelState.AddModelError("", "Email đã được dăng ký");
                    }

                }
            }
            return View("Register");
        }
    }
}