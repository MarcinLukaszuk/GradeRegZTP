using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GradeRegZTP.Models
{
    public class SubjectStudentGroupTeacher
    {
        [Key]
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public int StudentsGroupId { get; set; }
        public string TeacherID { get; set; }

        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }
        [ForeignKey("StudentsGroupId")]
        public virtual StudentsGroup StudentsGroup { get; set; }
    }
}