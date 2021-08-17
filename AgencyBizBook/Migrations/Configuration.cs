namespace AgencyBizBook.Migrations
{
    using AgencyBizBook.Entities;
    using AgencyBizBook.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
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

            //var passwordHasher = new PasswordHasher();
            //var user = new ApplicationUser();
            //user.Email = "fadi.butt001@gmail.com";
            //user.UserName = "fadi.butt001@gmail.com";
            //user.FirstName = "Fahad Butt";
            //user.LastName = "Ejaz Ahmed Butt";
            //user.PhoneNumber = "03005588999";
            //user.PasswordHash = passwordHasher.HashPassword("Admin123@");
            //user.SecurityStamp = Guid.NewGuid().ToString();
            //user.EmailConfirmed = true;
            //user.PhoneNumber = "032657158786";
            //user.Address = "45 ABC UXU";
            //user.CNIC = "454578784521657";
            

            //Step 2 Create and add the new Role.
            //var userRole = new IdentityRole("Driver");
            //context.Roles.Add(userRole);

            ////Step 3 Create a role for a user
            //var role = new IdentityUserRole();
            //role.RoleId = userRole.Id;
            //role.UserId = user.Id;

            ////Step 4 Add the role row and add the user to DB)
            //user.Roles.Add(role);

            //context.Users.AddOrUpdate(user);
            //context.SaveChanges();

            //context.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
