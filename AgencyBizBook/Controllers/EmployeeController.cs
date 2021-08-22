using AgencyBizBook.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgencyBizBook.Controllers
{
    public class EmployeeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private string connString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.ToString();
        // GET: Employee
        public ActionResult Index(bool drivers = false, bool salesMen = false, bool accountants = false)
        {
            string query = "\0";
            var modelList = new List<EmployeeIndexViewModel>();
            if (drivers)
            {
                query = @"SELECT u.Id, u.FirstName, u.LastName, u.Address, u.CNIC,
                                u.JoinDate, u.PhoneNumber, r.Name
                                FROM AspNetUsers u 
                                join AspNetUserRoles ur on u.id = ur.UserId
                                join AspNetRoles r on ur.RoleId = r.Id 
                                where r.Name != 'Admin'" + " AND r.Name = 'Driver' ;";
            }
            else if (salesMen)
            {
                query = @"SELECT u.Id, u.FirstName, u.LastName, u.Address, u.CNIC,
                                u.JoinDate, u.PhoneNumber, r.Name
                                FROM AspNetUsers u 
                                join AspNetUserRoles ur on u.id = ur.UserId
                                join AspNetRoles r on ur.RoleId = r.Id 
                                where r.Name != 'Admin'" + " AND r.Name = 'Salesman' ;";
            }
            else if (accountants)
            {
                query = @"SELECT u.Id, u.FirstName, u.LastName, u.Address, u.CNIC,
                                u.JoinDate, u.PhoneNumber, r.Name
                                FROM AspNetUsers u 
                                join AspNetUserRoles ur on u.id = ur.UserId
                                join AspNetRoles r on ur.RoleId = r.Id 
                                where r.Name != 'Admin'" + " AND r.Name = 'Accountant' ;";
            }
            else
            {
                query = @"SELECT u.Id, u.FirstName, u.LastName, u.Address, u.CNIC,
                                u.JoinDate, u.PhoneNumber, r.Name
                                FROM AspNetUsers u 
                                join AspNetUserRoles ur on u.id = ur.UserId
                                join AspNetRoles r on ur.RoleId = r.Id 
                                where r.Name != 'Admin';
                                ";
            }
            
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        var model = new EmployeeIndexViewModel();
                        model.EmployeeId = dr.GetString(0);
                        model.Name = dr.GetString(1) + " " + dr.GetString(2);
                        if (!dr.IsDBNull(dr.GetOrdinal("Address")))
                            model.Address = dr.GetString(3);
                        if (!dr.IsDBNull(dr.GetOrdinal("CNIC")))
                            model.CNIC = dr.GetString(4);
                        model.JoinDate = dr.GetDateTime(5);
                        model.PhoneNumber = dr.GetString(6);
                        model.Role = dr.GetString(7);
                        modelList.Add(model);
                    }
                }
                dr.Close();
                conn.Close();
            }
            return View(modelList);
        }


    }
}