namespace pet_web_shop.Migrations
{
    using pet_web_shop.Models.EF;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<pet_web_shop.Models.EF.PetShopDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(pet_web_shop.Models.EF.PetShopDBContext context)
        {
            tb_account user = new tb_account()
            {
                first_name = "Nguyen",
                last_name = "Khuyen",
                address = "Ha Noi",
                date_of_birth = new DateTime(2002, 01, 01),
                email = "yenhaikhuyen@gmail.com",
                avatar = null,
                phone_number = "0123456789",
                gender = 0,
                created = DateTime.Now,
                modified = null,
                user_name = "owner",
                password = "72122ce96bfec66e2396d2e25225d70a",
                role = 3,
                status = 1,
            };
            context.tb_account.AddOrUpdate(x => x.id,
                user);
        }
    }
}
