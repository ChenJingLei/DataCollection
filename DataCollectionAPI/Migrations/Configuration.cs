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

            var sysAdmin = new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "系统管理员" };
            var admin = new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "普通管理员" };
            var manager = new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "客户经理" };
            var apply = new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "客户端待授权者" };

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
