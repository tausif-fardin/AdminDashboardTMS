using AdminPart.Auth;
using AdminPart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdminPart.Models.Entity;
using System.Data.Entity;

namespace AdminPart.Controllers
{
    [AdminAccess]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            var user = GetUserList();
            var admin = GetAdminList();
            AdminUserModel model = new AdminUserModel();
            model.UsersModels = user;
            model.AdminModels = admin;

            return PartialView(model);
        }

        public List<User> GetUserList()
        {
            TMS1Entities db = new TMS1Entities();

            var u = (from e in db.Users
                     select e).ToList();
            return u;
        }

        public List<Admin> GetAdminList()
        {
            TMS1Entities db = new TMS1Entities();

            var u = (from e in db.Admins
                     where e.adminname != null
                     select e).ToList();
            return u;
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            TMS1Entities db = new TMS1Entities();
            var admin = (from s in db.Users
                         where s.userid == id
                         select s).FirstOrDefault();

            return View(admin);

        }
        [HttpPost]
        public ActionResult Edit(UserModel sub_s)
        {
            if (ModelState.IsValid)
            {
                TMS1Entities db = new TMS1Entities();

                var admin = (from s in db.Users
                             where s.userid == sub_s.userid
                             select s).FirstOrDefault();
                admin.userid = sub_s.userid;
                admin.username = sub_s.username;
                admin.password = sub_s.password;
                admin.role = sub_s.role;
                db.Entry(admin).CurrentValues.SetValues(sub_s);
                db.SaveChanges();

                var user = (from s in db.Admins
                             where s.userid == sub_s.userid
                             select s).FirstOrDefault();
                user.adminname = sub_s.username;
                user.password = sub_s.password;
                user.userid = sub_s.userid;
                db.Entry(user).CurrentValues.SetValues(sub_s);
                db.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public ActionResult Details()
        {
            if (Session["UserName"] != null)
            {
                UserModel am = new UserModel();
                TMS1Entities db = new TMS1Entities();

                var user = (from u in db.Users
                            select new
                            {
                                u.userid,
                                u.username,
                                u.password,
                            }).ToList();
                foreach (var item in user)
                {
                    if (item.username.Equals(Session["UserName"]))
                    {
                        am.userid = item.userid;
                        am.username = item.username;
                        am.password = item.password;

                    }
                }
                return View(am);
            }
            return RedirectToAction("../Accounts/Login");
        }
        [HttpPost]
        public ActionResult Details(UserModel profile)
        {
            if (Session["UserName"] != null)
            {

                if (ModelState.IsValid)
                {

                    TMS1Entities db = new TMS1Entities();
                    var obj = db.Users.Where(value => value.userid == profile.userid).FirstOrDefault();
                    obj.userid = profile.userid;
                    obj.username = profile.username;
                    obj.password = profile.password;
                    obj.role = profile.role;
                    db.Entry(obj).State = EntityState.Modified;
                    db.SaveChanges();
                    var obj1 = db.Admins.Where(value => value.userid == profile.userid).FirstOrDefault();
                    obj1.userid = profile.userid;
                    obj1.adminname = profile.username;
                    obj1.password = profile.password;
                    db.Entry(obj1).State = EntityState.Modified;
                    db.SaveChanges();


                    return RedirectToAction("Profiles");
                }
                return View("Edit", profile.userid);
            }
            return RedirectToAction("../Accounts/Login");
        }
        public ActionResult Delete(int Id)
        {
            using (TMS1Entities db = new TMS1Entities())
            {
                Admin s = (from st in db.Admins
                           where st.adminid == Id
                           select st).First();
                return View(s);
            }
        }
        [HttpPost]
        public ActionResult Delete(AdminModel s)
        {
            using (TMS1Entities db = new TMS1Entities())
            {
                Admin entity = (from st in db.Admins
                                  where st.adminid == s.adminid
                                  select st).FirstOrDefault();
                db.Admins.Remove(entity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}