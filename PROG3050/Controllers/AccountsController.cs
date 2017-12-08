﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PROG3050.DAL;
using PROG3050.Models;
using System.Data.SqlClient;

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
        public ActionResult SignUp(string Question, string Answer, [Bind(Include = "AccountName,Usergroup,UPassword,Registered,Email,Country,StateProvince,City,Address")] Account account)
        {
            if ((Question == null || Question == "") || (Answer == null || Answer == ""))
            {
                ViewBag.SecQError = "Security question & answer are required.";
                return View(account);
            }
            if(db.Accounts.Find(account.AccountName) != null)
            {
                ViewBag.AccountExistsError = "That account name already exists.";
                return View(account);
            }
            if (ModelState.IsValid)
            {
                account.Usergroup = "MEMBER";
                account.Registered = DateTime.Now;
                db.Accounts.Add(account);
                db.SaveChanges();
                SaveSecurityQuestion(account.AccountName, Question, Answer);
                return RedirectToAction("Login");
            }

            return View(account);
        }
        public void SaveSecurityQuestion(string account, string question, string answer)
        {
            SecurityQuestion secQ = new SecurityQuestion();
            secQ.AccountName = account;
            secQ.Question = question;
            secQ.Answer = answer;

            db.SecurityQuestions.Add(secQ);
            db.SaveChanges();
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

        public ActionResult UserProfile(string member)
        {
            if (member == null && Session["User"] == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account user;
            if (member != null)
                user = db.Accounts.Find(member);
            else
                user = db.Accounts.Find(Session["User"]);

            if (user == null)
            {
                return HttpNotFound();
            }
            else if (Session["User"] != null)
            {
                List<string> f = getFriendList(Session["User"].ToString());
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
                account.Usergroup = account.Usergroup.ToUpper();
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
        public ActionResult ForgotPassword()
        {
            return View();
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

        public List<string> getFriendList(string AccountName)
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=CVGS;Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT FriendedAccountName FROM Friend where AccountName='" + AccountName + "'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            conn.Open();

            reader = cmd.ExecuteReader();

            List<string> AccountNamesList = new List<string>();
            if (reader.Read())
            {
                AccountNamesList.Add(reader.GetValue(0).ToString());
            }

        }
    }

}

