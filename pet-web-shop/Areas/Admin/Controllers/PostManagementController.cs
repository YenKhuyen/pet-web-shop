using PagedList;
using pet_web_shop.Common;
using pet_web_shop.Models.DAO;
using pet_web_shop.Models.EF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pet_web_shop.Areas.Admin.Controllers
{
    public class PostManagementController : Controller
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

        // GET: Admin/PostManagement
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

            var dao = new Post_DAO();
            var list = dao.GetList(search);

            int pageNumber = (page ?? 1);
            int pageSize = 10;

            return View(list.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/PostManagement/Details/5
        public ActionResult Details(int id, string search, string currentFilter, int? page)
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

            var dao = new Post_DAO();
            var list = dao.GetComment(id, search, "desc");

            int pageNumber = (page ?? 1);
            int pageSize = 10;

            return View(list.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult RemoveComment(int id)
        {
            try
            {
                var authResult = Auth();
                if (authResult != null)
                {
                    return authResult;
                }

                var dao = new Post_DAO();
                var deleted = dao.RemoveComment(id);
                if (deleted)
                {
                    return Json(new { success = true, msg = "Xoá thành công!" });
                }

                return Json(new { success = false, msg = "Có lỗi xảy ra, vui lòng thử lại sau!" });
            }
            catch
            {
                return Json(new { success = false, msg = "Có lỗi xảy ra, vui lòng thử lại sau!" });
            }
        }

        // GET: Admin/PostManagement/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/PostManagement/Create
        [HttpPost]
        public ActionResult Create(tb_post post)
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
                    var dao = new Post_DAO();

                    post.created = DateTime.Now;

                    var session = Session[Constants.USER_SESSION] as UserLogin;
                    var user_dao = new User_DAO();
                    var user = user_dao.GetItemByID(session.id);

                    var created = dao.Add(post, user);
                    if (created != null)
                    {
                        return RedirectToAction("index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Tạo bài viết thành công!");
                    }
                }

                return View("Create");
            }
            catch
            {
                return View("Create");
            }
        }

        // GET: Admin/PostManagement/Edit/5
        public ActionResult Edit(int id)
        {
            var authResult = Auth();
            if (authResult != null)
            {
                return authResult;
            }

            {
                var dao = new Post_DAO();

                var post = dao.GetItemByID(id);
                if (post != null)
                    return View(post);
                return RedirectToAction("index");
            }
        }

        // POST: Admin/PostManagement/Edit/5
        [HttpPost]
        public ActionResult Edit(tb_post post)
        {
            try
            {
                var authResult = Auth();
                if (authResult != null)
                {
                    return authResult;
                }

                if (!ModelState.IsValid)
                {
                    // Log ModelState errors
                    foreach (var state in ModelState)
                    {
                        if (state.Value.Errors.Any())
                        {
                            foreach (var error in state.Value.Errors)
                            {
                                string errorMessage = error.ErrorMessage;
                                string propertyName = state.Key;

                                // Log or debug the error message and property name
                                // Example:
                                Debug.WriteLine($"Validation error for {propertyName}: {errorMessage}");
                            }
                        }
                    }
                }

                if (ModelState.IsValid)
                {
                    var dao = new Post_DAO();

                    if (post != null)
                    {
                        var updated = dao.Update(post);
                        if (updated != null)
                            return RedirectToAction("index");
                        else
                        {
                            ModelState.AddModelError("", "Cập nhật bài viết thất bại! Vui lòng thử lại sau!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Không tìm được bài viết! Vui lòng thử lại!");
                    }
                }

                return View(post);
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/PostManagement/Delete/5
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

                var dao = new Post_DAO();
                var deleted = dao.Detele(id);
                if (deleted)
                {
                    return Json(new { success = true, msg = "Xoá thành công!" });
                }

                return Json(new { success = false, msg = "Có lỗi xảy ra, vui lòng thử lại sau!" });
            }
            catch
            {
                return Json(new { success = false, msg = "Có lỗi xảy ra, vui lòng thử lại sau!" });
            }
        }
    }
}
