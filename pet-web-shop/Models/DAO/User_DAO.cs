using pet_web_shop.Common;
using pet_web_shop.Models.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using static pet_web_shop.Constants;

namespace pet_web_shop.Models.DAO
{
    public class User_DAO
    {
        private PetShopDBContext db = null;
        public User_DAO()
        {
            db = new PetShopDBContext();
        }

        public tb_account Add(tb_account account)
        {
            var acc = account;
            acc.password = Encryptor.MD5Hash(account.password);
            acc.created = DateTime.Now;
            db.tb_account.Add(account);
            db.SaveChanges();
            return account;
        }

        public tb_account Update(tb_account acc, string pass)
        {
            db.tb_account.Attach(acc);

            acc.password = acc.password != pass ? Encryptor.MD5Hash(acc.password) : acc.password;
            acc.modified = DateTime.Now;

            db.Entry(acc).Property(x => x.last_name).IsModified = true;
            db.Entry(acc).Property(x => x.address).IsModified = true;
            db.Entry(acc).Property(x => x.avatar).IsModified = true;
            db.Entry(acc).Property(x => x.date_of_birth).IsModified = true;
            db.Entry(acc).Property(x => x.email).IsModified = true;
            db.Entry(acc).Property(x => x.first_name).IsModified = true;
            db.Entry(acc).Property(x => x.gender).IsModified = true;
            db.Entry(acc).Property(x => x.role).IsModified = true;
            db.Entry(acc).Property(x => x.status).IsModified = true;
            db.Entry(acc).Property(x => x.modified).IsModified = true;
            db.Entry(acc).Property(x => x.password).IsModified = true;
            db.SaveChanges();
            return acc;
        }

        public dynamic Detele(int id)
        {
            var user = db.tb_account.FirstOrDefault(x => x.id == id);
            if (user.role == RoleAdmin)
            {
                return false;
            }

            user.status = 0;
            user.modified = new DateTime();
            db.SaveChanges();
            return user;
        }

        public dynamic Login(string user_name, string pass)
        {
            var user = db.tb_account.FirstOrDefault(x => x.user_name == user_name);

            if (user == null)
            {
                return -2; // Wrong user name
            }
            else
            {
                if (user.password != pass)
                {
                    return -1; // Wrong password
                }
                else
                {
                    if (user.status == DeactivatedUser)
                    {
                        return 0; // Deactivated account user
                    }
                    else return user; // Login success
                }



            }
        }

        public tb_account GetItemByNameAndEmail(string username, string email)
        {
            if (username != null)
                return db.tb_account.FirstOrDefault(x => x.user_name == username);
            return db.tb_account.FirstOrDefault(x => x.email == email);
        }

        public tb_account GetItemByID(int id)
        {
            return db.tb_account.FirstOrDefault(x => x.id == id);
        }

        public tb_account GetOwner()
        {
            return db.tb_account.Where(x => x.role == Constants.RoleOwner).First();
        }

        public List<tb_account> GetList(string search)
        {
            if (!String.IsNullOrEmpty(search))
            {
                return db.tb_account.Where(s => s.first_name.Contains(search) || s.last_name.Contains(search)).OrderBy(x => x.created).ToList();
            }
            return db.tb_account.OrderBy(x => x.created).ToList();
        }

        public void Detached(tb_account acc)
        {
            db.Entry(acc).State = EntityState.Detached;
        }

        public Boolean ChangePass(string pass, tb_account user)
        {
            try
            {
                if (user != null)
                {
                    user.password = Encryptor.MD5Hash(pass);
                    user.modified = DateTime.Now;

                    db.tb_account.Attach(user);

                    db.Entry(user).Property(x => x.password).IsModified = true;
                    db.Entry(user).Property(x => x.modified).IsModified = true;
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
    }
}