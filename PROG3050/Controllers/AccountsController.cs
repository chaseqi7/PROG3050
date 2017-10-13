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

        // GET: Accounts
        public ActionResult Index()
        {
            return View(db.Accounts.ToList());
        }

        // GET: Accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }


        // GET: Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountName,Usergroup,UPassword,Registered,Email,Country,StateProvince,City,Address")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // GET: Accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
