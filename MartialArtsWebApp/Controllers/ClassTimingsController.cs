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
    public class ClassTimingsController : Controller
    {
        private MarsEntities db = new MarsEntities();

        // GET: ClassTimings
        public ActionResult Index(string searchString)
        {
            var classTimings = from c in db.ClassTimings select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                classTimings = classTimings.Where(c => c.Class_Name.Contains(searchString)
                || c.Class_Day.Contains(searchString) || c.Class_Level.Contains(searchString));
            }
           
            return View(classTimings.ToList());
        }

        // GET: ClassTimings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassTiming classTiming = db.ClassTimings.Find(id);
            if (classTiming == null)
            {
                return HttpNotFound();
            }
            return View(classTiming);
        }

        // GET: ClassTimings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClassTimings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClassID,Class_Name,Class_Day,Class_Time,Class_Level")] ClassTiming classTiming)
        {
            if (ModelState.IsValid)
            {
                db.ClassTimings.Add(classTiming);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(classTiming);
        }

        // GET: ClassTimings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassTiming classTiming = db.ClassTimings.Find(id);
            if (classTiming == null)
            {
                return HttpNotFound();
            }
            return View(classTiming);
        }

        // POST: ClassTimings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClassID,Class_Name,Class_Day,Class_Time,Class_Level")] ClassTiming classTiming)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classTiming).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(classTiming);
        }

        // GET: ClassTimings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassTiming classTiming = db.ClassTimings.Find(id);
            if (classTiming == null)
            {
                return HttpNotFound();
            }
            return View(classTiming);
        }

        // POST: ClassTimings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassTiming classTiming = db.ClassTimings.Find(id);
            db.ClassTimings.Remove(classTiming);
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
