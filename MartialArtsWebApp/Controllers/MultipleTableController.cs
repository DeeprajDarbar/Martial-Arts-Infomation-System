using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MartialArtsWebApp.Models;

namespace MartialArtsWebApp.Controllers
{
    public class MultipleTableController : Controller
    {
        // GET: MultipleTable
        public ActionResult Index()
        {
            MarsEntities ms = new MarsEntities();
            List<Student> students = ms.Students.ToList();
            List<StudentRankDetail> rankDetails = ms.StudentRankDetails.ToList();
            List<Parent> parents = ms.Parents.ToList();

            ViewData["joinTables"] = from s in students join rd in rankDetails on s.StudentID equals rd.StudentID into table1
                                     from rd in table1.DefaultIfEmpty()
                                     join p in parents on rd.StudentID equals p.StudentID into table2
                                     from p in table2.DefaultIfEmpty()
                                     select new MultipleTableJoin { students = s, parents = p, rankDetails = rd };
            return View(ViewData["joinTables"]);
        }
    }
}