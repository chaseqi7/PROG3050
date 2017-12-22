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
using System.Data.SqlClient;

namespace PROG3050.Controllers
{

    public class GamesController : Controller
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
        // GET: Games
        public ActionResult Index(string search)
        {
            var games = from g in db.Games
                         select g;

            if (!String.IsNullOrEmpty(search))
            {
                games = games.Where(s => s.Title.Contains(search));
            }

            return View(games);
            
        }

        // GET: Games/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            else if (Session["User"]!=null)
            {
                string user = Session["User"].ToString();
                ViewBag.GameOwnership = getGameOwnership(user, game.GameID)==true ? "You already own this game" : "";
                ViewBag.GameInCart = IsGameInCart(user, game.GameID) == true ? "inCart" : "";
            }
            return View(game);
        }


        // GET: Games/Create
        public ActionResult Create()
        {
            if (!ValidateUserGroup(2))
                return RedirectToAction("Index", "Home");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GameID,Title,Description,Platform,Developer,Publisher,Genre,EsrbRating,Price,PublishDate")] Game game)
        {
            if (!ValidateUserGroup(2))
                return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(game);
        }

        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!ValidateUserGroup(2))
                return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GameID,Title,Platform,Description,Developer,Publisher,Genre,EsrbRating,Price,PublishDate")] Game game)
        {
            if (!ValidateUserGroup(2))
                return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(game);
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!ValidateUserGroup(2))
                return RedirectToAction("Index", "Home");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!ValidateUserGroup(2))
                return RedirectToAction("Index", "Home");
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
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
        
        public bool getGameOwnership(string AccountName, int GameID)
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=CVGS;Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT TransactionId FROM PaymentTransaction join PaymentInfo on PaymentTransaction.PaymentInfoId=PaymentInfo.PaymentInfoId where GameId=" + GameID + " and AccountName='" + AccountName+"'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            conn.Open();

            reader = cmd.ExecuteReader();
            
            if (reader.Read())
            {
                return true;
            }

            return false;
        }
   
        public bool IsGameInCart(string AccountName, int GameID)
        {
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=CVGS;Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT * FROM CartItems where AccountName='" + AccountName + "' and GameID=" +GameID;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            conn.Open();

            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return true;
            }

            return false;
        }
        public ActionResult AddGameToCart(int GameId) { 


            string user = Session["User"].ToString();


            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=CVGS;Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "INSERT INTO CartItems(AccountName,GameID) VALUES ('"+user+"',"+ GameId + ");";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            conn.Open();

            reader = cmd.ExecuteReader();
            
            return RedirectToAction("Index");
        }

        public ActionResult RemoveGameFromCart(int GameId)
        {
            string user = Session["User"].ToString();
            
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=CVGS;Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "DELETE FROM CartItems WHERE AccountName='"+user+"' and GameId="+GameId;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            conn.Open();

            reader = cmd.ExecuteReader();
            
            return RedirectToAction("Index");
        }

        
        public ActionResult Cart()
        {
            string user = Session["User"].ToString();

            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=CVGS;Integrated Security=True");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "SELECT GameId FROM CartItems where AccountName='" + user + "';";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;

            conn.Open();


            List<int> GameIDs = new List<int>();
           
            using (reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var myString = reader.GetInt32(0);
                    GameIDs.Add(myString);
                }
            }
            
            var games = from g in db.Games
                        select g;

            return View(games.Where(t => GameIDs.Contains(t.GameID)));            
        }
    }
}
