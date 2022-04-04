using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AdminPart.Auth;
using AdminPart.Models;
using AdminPart.Models.Entity;

namespace AdminPart.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        [HttpGet]
        public ActionResult Registration()
        {
            return View(new UserModel());
        }
        [HttpPost]
        public ActionResult Registration(UserModel user)
        {
            if (ModelState.IsValid)
            {
                var db = new TMS1Entities();

                var u = new User();
                u.password = user.password;
                u.username = user.username;
                u.role = "customer";
                db.Users.Add(u);
                db.SaveChanges();

                return RedirectToAction("Registration");

            }
            return View(user);

        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserModel user)
        {
            if (ModelState.IsValid)
            {
                var db = new TMS1Entities();
                var u = (from e in db.Users
                         where e.username.Equals(user.username) &&
                         e.password.Equals(user.password)
                         select e).FirstOrDefault();

                if(u != null)
                {
                    Session["UserName"] = u.username;
                    Session["UserId"] = u.userid;
                    Session["UserType"] = u.role;

                    if (u.role.Equals("customer"))
                    {
                        //Customer dashboard
                        //FormsAuthentication.SetAuthCookie(user.username, false);
                        
                        return RedirectToAction("Dashboard", "Admin");
                    }
                    else if (u.role.Equals("admin"))
                    {
                        var admin = (from e in db.Admins
                                     where e.userid==u.userid
                                     select e).FirstOrDefault();

                        Session["id"] = admin.adminid;
                        //Admin Dashboard
                        // FormsAuthentication.SetAuthCookie(user.username, false);
                        return RedirectToAction("Index", "Admin");
                    }
                }
                else
                {
                    ViewBag.Message = "Incorrect username/password";
                    return View();
                }
            }         
            return View();
        }

        public ActionResult LogOut()
        {
            Session.RemoveAll();
            return RedirectToAction("login");
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}