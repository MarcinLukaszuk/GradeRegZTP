using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GradeRegZTP.Models
{
    public partial class Grade
    {
        [Key]
        public int Id { get; set; }
        public int SubjectId { get; set; }

        public decimal Value { get; set; }
        public decimal Weight { get; set; }
        public string Note { get; set; }       
        public DateTime? Date { get; set; }
        public string Owner { get; set; }

        [ForeignKey("SubjectId")]
        public virtual Subject Subject { get; set; }
    }

    public partial class Grade
    {
        
    }
}