using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GradeRegZTP.Models
{
    public class HourOfDay
    {
        [Key]
        public int Id { get; set; }

        public int HourId { get; set; }
        public int SubjectId { get; set; }
        public int StudentsGroupId { get; set; }
        public int DayOfWeekId { get; set; }

        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }
        [ForeignKey("StudentsGroupId")]
        public virtual StudentsGroup StudentsGroup { get; set; }
        [ForeignKey("DayOfWeekId")]
        public virtual DayOfWeek DayOfWeek { get; set; }
        [ForeignKey("HourId")]
        public virtual Hour Hour { get; set; }
    }
}