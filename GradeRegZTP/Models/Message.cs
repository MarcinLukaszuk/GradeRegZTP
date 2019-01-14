using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GradeRegZTP.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string MessageText { get; set; }
        public DateTime Time { get; set; }
        public string SenderID { get; set; }
        public string RecieverID { get; set; }
    }
}