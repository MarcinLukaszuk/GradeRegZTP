using GradeRegZTP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GradeRegZTP.ViewModel
{
    public class StudentGradesViewModel
    {
        public IEnumerable<Grade> Grades { get; set; }
        public IEnumerable<Subject> Subjects { get; set; }
    }
}