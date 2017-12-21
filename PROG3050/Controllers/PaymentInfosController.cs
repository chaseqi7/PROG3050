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
    public class PaymentInfoController : Controller
    {
        private CVGSContext db = new CVGSContext();

        // GET: PaymentInfos
        public ActionResult Index()
        {
            var user = (String)Session["User"];
            return View(db.PaymentInfos.Where(p => p.AccountName == user).ToList());
        }

        // GET: PaymentInfos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentInfo paymentInfo = db.PaymentInfos.Find(id);
            if (paymentInfo == null)
            {
                return HttpNotFound();
            }
            // Validify User
            if(paymentInfo.AccountName != (String)Session["User"])
            {
                return HttpNotFound();
            }
            return View(paymentInfo);
        }

        // GET: PaymentInfos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentInfos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentInfoId,CardNumber,CardType,ExpirationDate,CVC")] PaymentInfo paymentInfo)
        {
            paymentInfo.AccountName = (String)Session["User"];
            if (ModelState.IsValid)
                {
                    db.PaymentInfos.Add(paymentInfo);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            return View(paymentInfo);
        }

        // GET: PaymentInfos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentInfo paymentInfo = db.PaymentInfos.Find(id);
            if (paymentInfo == null)
            {
                return HttpNotFound();
            }
            // Validify User
            if (paymentInfo.AccountName != (String)Session["User"])
            {
                return HttpNotFound();
            }
            return View(paymentInfo);
        }

        // POST: PaymentInfos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentInfoId,AccountName,CardNumber,CardType,ExpirationDate,CVC")] PaymentInfo paymentInfo)
        {
            // Validify User
            if (paymentInfo.AccountName == (String)Session["User"])
            {
                if (ModelState.IsValid)
                {
                    db.Entry(paymentInfo).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(paymentInfo);
        }

        // GET: PaymentInfos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentInfo paymentInfo = db.PaymentInfos.Find(id);
            if (paymentInfo == null)
            {
                return HttpNotFound();
            }
            // Validify User
            if (paymentInfo.AccountName != (String)Session["User"])
            {
                return HttpNotFound();
            }
            return View(paymentInfo);
        }

        // POST: PaymentInfos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentInfo paymentInfo = db.PaymentInfos.Find(id);
            // Validify User
            if (paymentInfo.AccountName != (String)Session["User"])
            {
                return HttpNotFound();
            }
            db.PaymentInfos.Remove(paymentInfo);
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
