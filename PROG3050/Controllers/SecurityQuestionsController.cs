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
    public class SecurityQuestionsController : Controller
    {
        private CVGSContext db = new CVGSContext();

        // GET: SecurityQuestions
        public ActionResult Index()
        {
            return View(db.SecurityQuestions.ToList());
        }

        // GET: SecurityQuestions/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SecurityQuestion securityQuestion = db.SecurityQuestions.Find(id);
            if (securityQuestion == null)
            {
                return HttpNotFound();
            }
            return View(securityQuestion);
        }

        // GET: SecurityQuestions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SecurityQuestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Question,Answer")] SecurityQuestion securityQuestion)
        {
            securityQuestion.AccountName = (string)Session["User"];
            if (db.SecurityQuestions.Find(securityQuestion.AccountName, securityQuestion.Question) != null)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                db.SecurityQuestions.Add(securityQuestion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(securityQuestion);
        }

        // GET: SecurityQuestions/Edit/5
        public ActionResult Edit(string account, string question)
        {
            if (account == null || question == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SecurityQuestion securityQuestion = db.SecurityQuestions.Find(account, question);
            if (securityQuestion == null)
            {
                return HttpNotFound();
            }
            return View(securityQuestion);
        }

        // POST: SecurityQuestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AccountName,Question,Answer")] SecurityQuestion securityQuestion)
        {
            if (db.SecurityQuestions.Find(securityQuestion.AccountName, securityQuestion.Question) != null)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                db.Entry(securityQuestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(securityQuestion);
        }

        // GET: SecurityQuestions/Delete/5
        public ActionResult Delete(string account, string question)
        {
            if (account == null || question == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SecurityQuestion securityQuestion = db.SecurityQuestions.Find(account, question);
            if (securityQuestion == null)
            {
                return HttpNotFound();
            }
            return View(securityQuestion);
        }

        // POST: SecurityQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string account, string question)
        {
            SecurityQuestion securityQuestion = db.SecurityQuestions.Find(account, question);
            db.SecurityQuestions.Remove(securityQuestion);
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
