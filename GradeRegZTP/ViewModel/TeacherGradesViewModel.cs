using GradeRegZTP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GradeRegZTP.ViewModel
{
    public class TeacherGradesViewModel
    {
        public Subject Subject { get; set; }
        public StudentsGroup StudentsGroup { get; set; }
    }
}