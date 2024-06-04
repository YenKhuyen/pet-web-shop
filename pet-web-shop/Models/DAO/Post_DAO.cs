using pet_web_shop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pet_web_shop.Models.DAO
{
    public class Post_DAO
    {
        private PetShopDBContext db = null;
        public Post_DAO()
        {
            db = new PetShopDBContext();
        }

        public tb_post Add(tb_post post, tb_account acc)
        {
            try
            {
                post.created = DateTime.Now;
                post.user_id = acc.id;
                db.tb_post.Add(post);
                acc.posts.Add(post);
                db.SaveChanges();
                return post;
            }
            catch
            {
                return null;
            }
        }

        public tb_post Update(tb_post post)
        {
            post.modified = DateTime.Now;

            db.tb_post.Attach(post);
            db.Entry(post).Property(x => x.title).IsModified = true;
            db.Entry(post).Property(x => x.contents).IsModified = true;
            db.Entry(post).Property(x => x.image).IsModified = true;
            db.Entry(post).Property(x => x.sub_title).IsModified = true;
            db.Entry(post).Property(x => x.modified).IsModified = true;

            db.SaveChanges();
            return post;
        }

        public Boolean Detele(int id)
        {
            var cate = db.tb_post.Find(id);
            if (cate != null)
            {
                db.tb_post.Remove(cate);
                db.SaveChanges();
                return true;
            }

            db.SaveChanges();
            return false;
        }

        public tb_post GetItem(string title)
        {
            return db.tb_post.FirstOrDefault(x => x.title == title);
        }

        public List<tb_post> GetList(string search)
        {
            if (!String.IsNullOrEmpty(search))
            {
                return db.tb_post.Where(s => s.title.Contains(search) || s.sub_title.Contains(search)).OrderBy(x => x.created).ToList();
            }
            return db.tb_post.OrderBy(x => x.created).ToList();
        }

        public tb_post GetItemByID(int id)
        {
            return db.tb_post.Find(id);
        }
        public List<tb_post> GetTopPost()
        {
            return db.tb_post.OrderByDescending(x => x.created).Take(5).ToList();
        }

        public List<tb_review> GetComment(int id, string search, string sort = "asc")
        {
            if (search != null)
            {
                var list = db.tb_post.Find(id).reviews.Where(x => x.comment.Contains(search) || x.account.first_name.Contains(search) || x.account.last_name.Contains(search));
                return sort == "asc" ? list.OrderBy(x => x.created).ToList() : list.OrderByDescending(x => x.created).ToList();
            }

            var list_ = db.tb_post.Find(id).reviews;
            return sort == "asc" ? list_.OrderBy(x => x.created).ToList() : list_.OrderByDescending(x => x.created).ToList();
        }

        public Boolean RemoveComment(int id)
        {
            var comment = db.tb_review.Find(id);
            if (comment != null)
            {
                db.tb_review.Remove(comment);
                db.SaveChanges();
                return true;
            }

            db.SaveChanges();
            return false;
        }

        public Boolean CreateCommnet(tb_post post, string comment, int user_id)
        {
            try
            {
                var acc = db.tb_account.Find(user_id);

                if (acc == null)
                {
                    return false;
                }
                tb_review new_comment = new tb_review
                {
                    created = DateTime.Now,
                    comment = comment,
                    post_id = post.id,
                    user_id = user_id,
                };
                post.reviews.Add(new_comment);
                acc.review.Add(new_comment);
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}