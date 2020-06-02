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
    public class TestFeesController : Controller
    {
        private MarsEntities db = new MarsEntities();

        // GET: TestFees
        public ActionResult Index()
        {
            var testFees = db.TestFees.Include(t => t.Rank);
            return View(testFees.ToList());
        }

        // GET: TestFees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestFee testFee = db.TestFees.Find(id);
            if (testFee == null)
            {
                return HttpNotFound();
            }
            return View(testFee);
        }

        // GET: TestFees/Create
        public ActionResult Create()
        {
            ViewBag.RankID = new SelectList(db.Ranks, "RankID", "Rank_Name");
            return View();
        }

        // POST: TestFees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TestID,TestFees,RankID")] TestFee testFee)
        {
            if (ModelState.IsValid)
            {
                db.TestFees.Add(testFee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RankID = new SelectList(db.Ranks, "RankID", "Rank_Name", testFee.RankID);
            return View(testFee);
        }

        // GET: TestFees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestFee testFee = db.TestFees.Find(id);
            if (testFee == null)
            {
                return HttpNotFound();
            }
            ViewBag.RankID = new SelectList(db.Ranks, "RankID", "Rank_Name", testFee.RankID);
            return View(testFee);
        }

        // POST: TestFees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TestID,TestFees,RankID")] TestFee testFee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testFee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RankID = new SelectList(db.Ranks, "RankID", "Rank_Name", testFee.RankID);
            return View(testFee);
        }

        // GET: TestFees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestFee testFee = db.TestFees.Find(id);
            if (testFee == null)
            {
                return HttpNotFound();
            }
            return View(testFee);
        }

        // POST: TestFees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestFee testFee = db.TestFees.Find(id);
            db.TestFees.Remove(testFee);
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
