using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MartialArtsWebApp.Models;

namespace MartialArtsWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string searchString)
        {


            MarsEntities ms = new MarsEntities();
            List<Student> students = ms.Students.ToList();
            List<StudentRankDetail> rankDetails = ms.StudentRankDetails.ToList();
            List<Parent> parents = ms.Parents.ToList();

            var allDetails = from s in students
                                     join rd in rankDetails on s.StudentID equals rd.StudentID into table1
                                     from rd in table1.DefaultIfEmpty()
                                     join p in parents on s.StudentID equals p.StudentID into table2
                                     from p in table2.DefaultIfEmpty()
                                     select new MultipleTableJoin { students = s, parents = p, rankDetails = rd };
            
            if (!String.IsNullOrEmpty(searchString))
            {
                //IEnumerable<MultipleTableJoin> allDetails = from i in ViewData as SelectList where i.Key == "joinTables" select i.Value;
                //ViewData["joinTables"] = from item in ViewData["joinTables"] as allDetails where item.StudentName = searchString or 
                //allDetails = from item in allDetails where item.students.StudentName == searchString select item;
                searchString = searchString.ToLower();
                allDetails = allDetails.Where(p => p.students.StudentName.ToLower().Contains(searchString) || p.rankDetails.Rank.Rank_Name.ToLower().Contains(searchString)
                || p.students.Student_Phone.ToString().ToLower().Contains(searchString));
                //allDetails = allDetails.Where(p => p.Father_Name.Contains(searchString)
                //|| p.Mother_Name.Contains(searchString) || p.Parent_Email.Contains(searchString));
            }
            return View(allDetails);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}