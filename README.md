# AgencyBizBook - How to Run

## Step 1

- Open Package Manage Console (Tools>Nuget Package Manager>Package Manager Console)

```c#
enable-migrations
```

- A new Folder will be genrated in the solution named as Migrations it will have a file Configuration.cs open that file and change ```c# AutomaticMigrationsEnabled ``` to ```c# true ```
