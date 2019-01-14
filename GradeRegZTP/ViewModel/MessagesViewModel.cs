using GradeRegZTP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GradeRegZTP.ViewModel
{
    public class MessagesViewModel
    {
        public IEnumerable<Message> SentMessages { get; set; }
        public IEnumerable<Message> RecievedMessages { get; set; }
    }
}