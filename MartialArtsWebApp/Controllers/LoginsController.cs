using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MartialArtsWebApp.Models;

namespace MartialArtsWebApp.Controllers
{
    public class LoginsController : Controller
    {
        private MarsEntities db = new MarsEntities();

        // GET: Logins
        public ActionResult Index()
        {
            return View(db.Logins.ToList());
        }



        // GET: Logins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Logins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "username,password")] Login login)
        {
            if (ModelState.IsValid)
            {
                if (login.username == "admin" && login.password == "admin")
                    return RedirectToAction("../Home/Index");
            }

            return View(login);
        }
    }
}
