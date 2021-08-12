namespace AgencyBizBook.Migrations
{
    using AgencyBizBook.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AgencyBizBook.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(AgencyBizBook.Models.ApplicationDbContext context)
        {
            //var ec = new ExpenseCategory() { Name = "Utility Bills" };
            //var ec1 = new ExpenseCategory() { Name = "Guest" };
            //var ec2 = new ExpenseCategory() { Name = "Rent" };
            //context.ExpenseCategories.Add(ec2);
            //context.ExpenseCategories.Add(ec1);
            //context.ExpenseCategories.Add(ec);
            //context.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
