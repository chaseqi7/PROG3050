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
    public class ReportsController : Controller
    {
        private CVGSContext db = new CVGSContext();
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult GameList()
        {
            return View(db.Games.ToList());
        }

        public ActionResult NewGameList()
        {
            return View(db.Games.ToList());
        }

        public ActionResult MemberList()
        {
            return View(db.Accounts.ToList());
        }
        public ActionResult NewMemberList()
        {
            return View(db.Accounts.ToList());
        }

        public ActionResult UpcomingEventList()
        {
            return View(db.Events.ToList());
        }

    }
}
