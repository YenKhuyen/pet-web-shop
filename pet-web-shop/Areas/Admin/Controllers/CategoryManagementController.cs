using PagedList;
using pet_web_shop.Common;
using pet_web_shop.Models.DAO;
using pet_web_shop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pet_web_shop.Areas.Admin.Controllers
{
    public class CategoryManagementController : Controller
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

        // GET: Admin/CategoryManagement
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

            var dao = new Category_DAO();
            var userList = dao.GetList(search);

            int pageNumber = (page ?? 1);
            int pageSize = 10;

            return View(userList.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(tb_category cate)
        {
            var authResult = Auth();
            if (authResult != null)
            {
                return authResult;
            }

            if (ModelState.IsValid)
            {
                var dao = new Category_DAO();

                var checkName = dao.GetItem(cate.title);

                if (checkName == null)
                {
                    var ua = dao.Add(cate);
                    if (ua != null)
                    {
                        return RedirectToAction("index", "categorymanagement");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm danh mục thành công!");
                    }
                }
                else
                {
                    if (checkName != null)
                    {
                        ModelState.AddModelError("", "Tiêu đề danh mục đã tồn tại!");
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

                var dao = new Category_DAO();

                var cate = dao.GetItemByID(id ?? 0);
                if (cate != null)
                    return View(cate);
            }
            return RedirectToAction("index", "categorymanagement");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tb_category cate)
        {
            var authResult = Auth();
            if (authResult != null)
            {
                return authResult;
            }

            if (ModelState.IsValid)
            {
                var dao = new Category_DAO();

                if (cate != null)
                {
                    var updated = dao.Update(cate);
                    if (updated != null)
                        return RedirectToAction("index", "categorymanagement");
                    else
                    {
                        ModelState.AddModelError("", "Cập nhật thông tin thất bại, vui lòng thử lại sau!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Không tìm thấy danh mục! Vui lòng thử lại sau!");
                }
            }
            return View(cate);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var authResult = Auth();
            if (authResult != null)
            {
                return Json(new { success = false });
            }
            var dao = new Category_DAO();
            var cate = dao.GetItemByID(id);

            if (cate != null)
            {
                var deleted = dao.Detele(id);
                return Json(new { success = deleted });
            }
            return Json(new { success = false });
        }
    }
}