using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GradeRegZTP.Models
{
    public class Hour
    {
        [Key]
        public int Id { get; set; }
        public string HourString { get; set; }
    }
}