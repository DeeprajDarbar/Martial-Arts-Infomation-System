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
    public class StudentAttendencesController : Controller
    {
        private MarsEntities db = new MarsEntities();

        // GET: StudentAttendences
        public ActionResult Index(string searchString, string searchDate)
        {
            var studentAttendences = db.StudentAttendences.Include(s => s.ClassTiming).Include(s => s.Student);
            studentAttendences = from s in db.StudentAttendences select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                studentAttendences = studentAttendences.Where(s => s.Student.StudentName.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(searchDate))
            {
                DateTime date = Convert.ToDateTime(searchDate);
                studentAttendences = studentAttendences.Where(s => s.Student_datetime == date || s.Student_datetime == date);
            }
            return View(studentAttendences.ToList());
        }

        // GET: StudentAttendences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAttendence studentAttendence = db.StudentAttendences.Find(id);
            if (studentAttendence == null)
            {
                return HttpNotFound();
            }
            return View(studentAttendence);
        }

        // GET: StudentAttendences/Create
        public ActionResult Create()
        {
            ViewBag.ClassID = new SelectList(db.ClassTimings, "ClassID", "Class_Name");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName");
            return View();
        }

        // POST: StudentAttendences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Attendence_ID,StudentID,Student_datetime,ClassID")] StudentAttendence studentAttendence)
        {
            if (ModelState.IsValid)
            {
                db.StudentAttendences.Add(studentAttendence);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.ClassTimings, "ClassID", "Class_Name", studentAttendence.ClassID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName", studentAttendence.StudentID);
            return View(studentAttendence);
        }

        // GET: StudentAttendences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAttendence studentAttendence = db.StudentAttendences.Find(id);
            if (studentAttendence == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassID = new SelectList(db.ClassTimings, "ClassID", "Class_Name", studentAttendence.ClassID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName", studentAttendence.StudentID);
            return View(studentAttendence);
        }

        // POST: StudentAttendences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Attendence_ID,StudentID,Student_datetime,ClassID")] StudentAttendence studentAttendence)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentAttendence).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.ClassTimings, "ClassID", "Class_Name", studentAttendence.ClassID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName", studentAttendence.StudentID);
            return View(studentAttendence);
        }

        // GET: StudentAttendences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAttendence studentAttendence = db.StudentAttendences.Find(id);
            if (studentAttendence == null)
            {
                return HttpNotFound();
            }
            return View(studentAttendence);
        }

        // POST: StudentAttendences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentAttendence studentAttendence = db.StudentAttendences.Find(id);
            db.StudentAttendences.Remove(studentAttendence);
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
