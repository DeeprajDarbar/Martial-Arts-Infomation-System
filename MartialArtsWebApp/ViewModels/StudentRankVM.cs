using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MartialArtsWebApp.ViewModels
{
    public class StudentRankVM
    {
        public string StudentName { get; set; }
        public Nullable<int> Student_Phone { get; set; }
        public string Student_Email { get; set; }

        public Nullable<int> RankID { get; set; }
    }
}