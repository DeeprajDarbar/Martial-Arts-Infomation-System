
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MartialArtsWebApp.Models
{
    public class MultipleTableJoin
    {
        public Student students { get; set; }

        public StudentRankDetail rankDetails { get; set; }

        public Parent parents { get; set; }
 
    }
}