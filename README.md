# AgencyBizBook

## How to Start Application

- Open Package Manager Console (Tools>Nuget Package Manager>Package Manager Console)
- Write ```c#enable-migrations```
- A new Folder will be genrated in the solution named as Migrations it will have a file Configuration.cs open that file and change ```c#AutomaticMigrationsEnabled``` to ```c#true```
- Add the following code to ```c#Seed()``` method to make an admin account for login.

```c#
var passwordHasher = new PasswordHasher();
var user = new ApplicationUser();
user.Email = "admin@yahoo.com";
user.UserName = "admin@yahoo.com";
user.FirstName = "Admin";
user.LastName = "User";
user.PhoneNumber = "03231234567";
user.PasswordHash = passwordHasher.HashPassword("Admin123@");
user.SecurityStamp = Guid.NewGuid().ToString();
user.EmailConfirmed = true;
user.JoinDate = DateTime.Now;

var userRole = new IdentityRole("Admin");
context.Roles.Add(userRole);

var role = new IdentityUserRole();
role.RoleId = userRole.Id;
role.UserId = user.Id;

user.Roles.Add(role);
context.Users.Add(user);
context.SaveChanges();
```

- Reopen the Package Manager Console and write  ```c#update-database```
- Now your project is ready to run. Enjoy!
