using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PROG3050.DAL;
using PROG3050.Models;

namespace PROG3050.Controllers
{
    public class AccountsController : Controller
    {
        private CVGSContext db = new CVGSContext();

        [HttpPost]
        public ActionResult try_Login(string username, string password)
        {
            var cred_query = db.Accounts.Where(i => i.AccountName == username && i.UPassword == password);
            if (cred_query.Count() != 0)
            {
                // Valid user! Save their junk!
                Session["User"] = username;
                var usergroup = cred_query.First().Usergroup;
                Session["Usergroup"] = usergroup;
                Session["Permissions"] = db.Usergroup.Where(i => i.Title == usergroup).First().GroupPermissions;
                return RedirectToAction("Index", "Home");
            }
            TempData["UserMessage"] = "INVALID";
            return RedirectToAction("Login");
        }
        public ActionResult click_SignUp()
        {
            return View("~/Views/Accounts/SignUp.cshtml");
        }

        // GET: Accounts/Create
        public ActionResult SignUp()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp([Bind(Include = "AccountName,Usergroup,UPassword,Registered,Email,Country,StateProvince,City,Address")] Account account)
        {
            if (ModelState.IsValid)
            {

                account.Usergroup = "MEMBER";
                account.Registered = DateTime.Now;
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(account);
        }

        public ActionResult Login()
        {
            // Error Messages. A lil sloppy, but eh!
            if ((string)TempData["UserMessage"] == "INVALID")
                ViewBag.UserMessage = "Username or password is invalid.";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult UserProfile()
        {
            if (Session["User"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account user = db.Accounts.Find(Session["User"]);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountName,Usergroup,UPassword,Registered,Email,Country,StateProvince,City,Address")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserProfile");
            }
            return View(account);
        }

        public ActionResult ChangePassword()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Account user = db.Accounts.Find(Session["User"]);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string confirmPassword, [Bind(Include = "AccountName,Usergroup,UPassword,Registered,Email,Country,StateProvince,City,Address")] Account account)
        {
            if (account.UPassword == confirmPassword && ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserProfile");
            }
            return View(account);
        }

    }
}
