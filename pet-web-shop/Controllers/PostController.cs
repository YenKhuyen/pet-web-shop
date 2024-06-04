using PagedList;
using pet_web_shop.Models.DAO;
using pet_web_shop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pet_web_shop.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index(string search, string currentFilter, int? page)
        {
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

        public ActionResult Details(int id, string currentFilter, int? page)
        {
            ViewBag.CurrentFilter = currentFilter;

            int pageNumber = (page ?? 1);
            int pageSize = 10;

            var dao = new Post_DAO();
            var cate_dao = new Category_DAO();

            var top_post = dao.GetTopPost();
            var detail = dao.GetItemByID(id);
            var cate_list = cate_dao.GetList("");
            var list_comment = dao.GetComment(id, null);

            if (detail == null)
            {
                Redirect("/");
            }

            var DetailData = new PostDetailViewModels
            {
                CategoryList = cate_list,
                PostDetail = detail,
                PostList = top_post,
                ListCommnet = list_comment.OrderByDescending(x=>x.created).ToPagedList(pageNumber, pageSize),
            };

            return View(DetailData);
        }

        // GET: Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Post/Create
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

        // GET: Post/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Post/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Post/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Post/Delete/5
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
