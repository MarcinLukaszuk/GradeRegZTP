using GradeRegZTP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GradeRegZTP.ViewModel
{
    public class StudentsGroupAddGradeViewModelList
    {
        public StudentsGroupAddGradeViewModelList()
        {
            StudentsGroupAddGradeViewModels = new List<StudentsGroupAddGradeViewModel>();
        }
        public virtual List<StudentsGroupAddGradeViewModel> StudentsGroupAddGradeViewModels { get; set; }
    }
    public class StudentsGroupAddGradeViewModel
    {
        public StudentsGroupAddGradeViewModel()
        {
        }
        public string MyUserId { get; set; }
        public MyUser MyUser { get; set; }
        public decimal? Grade { get; set; }
        public string Note { get; set; }
        public decimal? Weight { get; set; }
        public string StudentGroupName { get; set; }
        public string SubjectName { get; set; }
        public int StudentsGroupId { get; set; }
        public int SubjectId { get; set; }
    }
}