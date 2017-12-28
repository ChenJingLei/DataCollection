namespace DataCollectionAPI.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataCollectionAPI.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataCollectionAPI.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var sysAdmin= new IdentityRole { Id = "c04e2839-d9f7-42bc-a7e2-9c8bcee1040b", Name = "系统管理员" };
            var admin = new IdentityRole { Id = "1614e96a-ce16-45a8-99db-ffbcb1d53718", Name = "普通管理员" };
            var manager = new IdentityRole { Id = "b3000fd7-2ac5-409a-b582-d3c2d8457459", Name = "客户经理" };
            var apply = new IdentityRole { Id = "d803555e-7801-45ae-b219-ec1cd40dd170", Name = "客户端待授权者" };

            context.Roles.AddOrUpdate(
                    r => r.Id,
                    sysAdmin,
                    admin,
                    manager,
                    apply
                );
        }
    }
}
