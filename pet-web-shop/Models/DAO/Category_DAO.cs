using pet_web_shop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pet_web_shop.Models.DAO
{
    public class Category_DAO
    {
        private PetShopDBContext db = null;
        public Category_DAO()
        {
            db = new PetShopDBContext();
        }

        public tb_category Add(tb_category category)
        {
            category.created = DateTime.Now;
            db.tb_category.Add(category);
            db.SaveChanges();
            return category;
        }

        public tb_category Update(tb_category category)
        {
            category.modified = DateTime.Now;

            db.tb_category.Attach(category);
            db.Entry(category).Property(x => x.title).IsModified = true;
            db.Entry(category).Property(x => x.description).IsModified = true;
            db.Entry(category).Property(x => x.modified).IsModified = true;

            db.SaveChanges();
            return category;
        }

        public Boolean Detele(int id)
        {
            var cate = db.tb_category.Find(id);
            if (cate != null)
            {
                db.tb_category.Remove(cate);
                db.SaveChanges();
                return true;
            }

            db.SaveChanges();
            return false;
        }

        public tb_category GetItem(string name)
        {
            return db.tb_category.FirstOrDefault(x => x.title == name);
        }

        public List<tb_category> GetList(string search)
        {
            if (!String.IsNullOrEmpty(search))
            {
                return db.tb_category.Where(s => s.title.Contains(search)).OrderBy(x => x.created).ToList();
            }
            return db.tb_category.OrderBy(x => x.created).ToList();
        }

        public tb_category GetItemByID(int id)
        {
            return db.tb_category.Find(id);
        }

        public List<tb_product> GetSearchProduct(string search, int cate_id)
        {
            if (!String.IsNullOrEmpty(search))
                return db.tb_category.Find(cate_id).product.Where(x => x.title.ToLower().Contains(search)).OrderBy(x => x.created).ToList();
            return db.tb_category.Find(cate_id).product.OrderBy(x => x.created).ToList();
        }
    }
}