using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GradeRegZTP.Models
{
    public class DayOfWeek
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<HourOfDay> HourOfDays { get; set; }

    }
}