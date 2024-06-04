using pet_web_shop.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pet_web_shop.Models.DAO
{
    public class About_DAO
    {
        private PetShopDBContext db = null;
        public About_DAO()
        {
            db = new PetShopDBContext();
        }

        public bool Update(tb_about about)
        {
            try
            {
                var count = db.tb_about.Count();
                if (count == 0)
                {
                    about.created = DateTime.Now;
                    var new_about = db.tb_about.Add(about);
                    db.SaveChanges();

                    return true;
                }
                else
                {
                    about.modified = DateTime.Now;

                    db.tb_about.Attach(about);
                    db.Entry(about).Property(x => x.contents).IsModified = true;
                    db.Entry(about).Property(x => x.modified).IsModified = true;

                    db.SaveChanges();

                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public tb_about GetItem()
        {
            try
            {
                return db.tb_about.First();
            }
            catch
            {
                return null;
            }
        }
    }
}