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
    public class MembersController : Controller
    {
        private CVGSContext db = new CVGSContext();
        // GET: Members
        public ActionResult Index(string search)
        {
            var members = from m in db.Accounts
                        select m;

            if (!String.IsNullOrEmpty(search))
            {
                members = members.Where(s => s.AccountName.Contains(search));
            }

            return View(members);
        }

        // GET: Members/Details/5
        
    }
}
