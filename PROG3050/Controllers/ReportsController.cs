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
        //Quick & easy permission validator
        private Boolean ValidateUserGroup(int perms)
        {
            if (Session["Permissions"] != null && (int)Session["Permissions"] >= perms)
            {
                return true;
            }
            return false;
        }
        // GET: Reports
        public ActionResult Index()
        {
            if (!ValidateUserGroup(2))
                return RedirectToAction("Index", "Home");
            return View();
        }
        
        public ActionResult GameList()
        {
            if (!ValidateUserGroup(2))
                return RedirectToAction("Index", "Home");
            return View(db.Games.ToList());
        }

        public ActionResult NewGameList()
        {
            if (!ValidateUserGroup(2))
                return RedirectToAction("Index", "Home");
            return View(db.Games.ToList());
        }

        public ActionResult MemberList()
        {
            if (!ValidateUserGroup(2))
                return RedirectToAction("Index", "Home");
            return View(db.Accounts.ToList());
        }
        public ActionResult NewMemberList()
        {
            if (!ValidateUserGroup(2))
                return RedirectToAction("Index", "Home");
            return View(db.Accounts.ToList());
        }

        public ActionResult UpcomingEventList()
        {
            if (!ValidateUserGroup(2))
                return RedirectToAction("Index", "Home");
            return View(db.Events.ToList());
        }

    }
}
