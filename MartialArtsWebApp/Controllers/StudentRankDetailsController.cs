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
    public class StudentRankDetailsController : Controller
    {
        private MarsEntities db = new MarsEntities();

        // GET: StudentRankDetails
        public ActionResult Index(string searchString)
        {
            var studentRankDetails = db.StudentRankDetails.Include(s => s.Rank).Include(s => s.Student);
            studentRankDetails = from s in db.StudentRankDetails select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                studentRankDetails = studentRankDetails.Where(s => s.Student.StudentName.Contains(searchString));
            }
            return View(studentRankDetails.ToList());

        }

        // GET: StudentRankDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentRankDetail studentRankDetail = db.StudentRankDetails.Find(id);
            if (studentRankDetail == null)
            {
                return HttpNotFound();
            }
            return View(studentRankDetail);
        }

        // GET: StudentRankDetails/Create
        public ActionResult Create()
        {
            ViewBag.RankID = new SelectList(db.Ranks, "RankID", "Rank_Name");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName");
            return View();
        }

        // POST: StudentRankDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentRankDetialID,StudentID,RankID")] StudentRankDetail studentRankDetail)
        {
            if (ModelState.IsValid)
            {
                db.StudentRankDetails.Add(studentRankDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RankID = new SelectList(db.Ranks, "RankID", "Rank_Name", studentRankDetail.RankID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName", studentRankDetail.StudentID);
            return View(studentRankDetail);
        }

        // GET: StudentRankDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentRankDetail studentRankDetail = db.StudentRankDetails.Find(id);
            if (studentRankDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.RankID = new SelectList(db.Ranks, "RankID", "Rank_Name", studentRankDetail.RankID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName", studentRankDetail.StudentID);
            return View(studentRankDetail);
        }

        // POST: StudentRankDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentRankDetialID,StudentID,RankID")] StudentRankDetail studentRankDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentRankDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RankID = new SelectList(db.Ranks, "RankID", "Rank_Name", studentRankDetail.RankID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName", studentRankDetail.StudentID);
            return View(studentRankDetail);
        }

        // GET: StudentRankDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentRankDetail studentRankDetail = db.StudentRankDetails.Find(id);
            if (studentRankDetail == null)
            {
                return HttpNotFound();
            }
            return View(studentRankDetail);
        }

        // POST: StudentRankDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentRankDetail studentRankDetail = db.StudentRankDetails.Find(id);
            db.StudentRankDetails.Remove(studentRankDetail);
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
