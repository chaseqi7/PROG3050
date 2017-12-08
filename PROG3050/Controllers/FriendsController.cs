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
    public class FriendsController : Controller
    {
        private CVGSContext db = new CVGSContext();

        // GET: Friends
        public ActionResult Index()
        {
            var friends = from f in db.Friends
                        select f;

            string username=Session["User"].ToString();
            friends = friends.Where(f => f.AccountName.Equals(username));

            return View(friends);
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
