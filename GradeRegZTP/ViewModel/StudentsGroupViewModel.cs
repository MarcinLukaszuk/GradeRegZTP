using GradeRegZTP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GradeRegZTP.ViewModel
{
    public class StudentGroupViewModel
    {
        public StudentGroupViewModel()
        {
            Grades = new List<Grade>();
        }
        public MyUser MyUser { get; set; }
        public List<Grade> Grades { get; set; }
        public string StudentGroupName { get; set; }
        public string SubjectName { get; set; }

        public int StudentGroupId { get; set; }
        public int SubjectId { get; set; }
    }
}