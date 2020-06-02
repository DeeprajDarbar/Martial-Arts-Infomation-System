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
    public class StudentFeesController : Controller
    {
        private MarsEntities db = new MarsEntities();

        // GET: StudentFees
        public ActionResult Index(string dateFrom, string dateTo)
        {
            var studentFees = db.StudentFees.Include(s => s.Inventory).Include(s => s.MembershipType).Include(s => s.Student).Include(s => s.TestFee);
            studentFees = from s in db.StudentFees select s;
            if (!String.IsNullOrEmpty(dateFrom))
            {
                DateTime date = Convert.ToDateTime(dateFrom).Date;
                studentFees = studentFees.Where(s => s.StudentFee_datetime >= date);
            }
            if (!String.IsNullOrEmpty(dateTo))
            {
                DateTime date1 = Convert.ToDateTime(dateTo).Date;
                date1 = date1.Date.AddDays(1).AddTicks(-1);
                studentFees = studentFees.Where(s => s.StudentFee_datetime <= date1);
            }
            return View(studentFees.ToList());
        }

        // GET: StudentFees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentFee studentFee = db.StudentFees.Find(id);
            if (studentFee == null)
            {
                return HttpNotFound();
            }
            return View(studentFee);
        }

        // GET: StudentFees/Create
        public ActionResult Create()
        {
            ViewBag.InventoryID = new SelectList(db.Inventories, "InventoryID", "Product_Name");
            ViewBag.MembershipID = new SelectList(db.MembershipTypes, "MembershipID", "Membership_Name");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName");
            ViewBag.TestID = new SelectList(db.TestFees, "TestID", "TestID");
            return View();
        }

        // POST: StudentFees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentFeeID,StudentID,TestID,MembershipID,InventoryID,StudentFee_datetime")] StudentFee studentFee)
        {
            if (ModelState.IsValid)
            {
                db.StudentFees.Add(studentFee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InventoryID = new SelectList(db.Inventories, "InventoryID", "Product_Name", studentFee.InventoryID);
            ViewBag.MembershipID = new SelectList(db.MembershipTypes, "MembershipID", "Membership_Name", studentFee.MembershipID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName", studentFee.StudentID);
            ViewBag.TestID = new SelectList(db.TestFees, "TestID", "TestID", studentFee.TestID);
            return View(studentFee);
        }

        // GET: StudentFees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentFee studentFee = db.StudentFees.Find(id);
            if (studentFee == null)
            {
                return HttpNotFound();
            }
            ViewBag.InventoryID = new SelectList(db.Inventories, "InventoryID", "Product_Name", studentFee.InventoryID);
            ViewBag.MembershipID = new SelectList(db.MembershipTypes, "MembershipID", "Membership_Name", studentFee.MembershipID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName", studentFee.StudentID);
            ViewBag.TestID = new SelectList(db.TestFees, "TestID", "TestID", studentFee.TestID);
            return View(studentFee);
        }

        // POST: StudentFees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentFeeID,StudentID,TestID,MembershipID,InventoryID,StudentFee_datetime")] StudentFee studentFee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentFee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InventoryID = new SelectList(db.Inventories, "InventoryID", "Product_Name", studentFee.InventoryID);
            ViewBag.MembershipID = new SelectList(db.MembershipTypes, "MembershipID", "Membership_Name", studentFee.MembershipID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "StudentName", studentFee.StudentID);
            ViewBag.TestID = new SelectList(db.TestFees, "TestID", "TestID", studentFee.TestID);
            return View(studentFee);
        }

        // GET: StudentFees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentFee studentFee = db.StudentFees.Find(id);
            if (studentFee == null)
            {
                return HttpNotFound();
            }
            return View(studentFee);
        }

        // POST: StudentFees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentFee studentFee = db.StudentFees.Find(id);
            db.StudentFees.Remove(studentFee);
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
