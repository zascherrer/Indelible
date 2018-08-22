using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Indelible.Models
{
    public class Email
    {
        [Key]
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string RecipientEmail { get; set; }
        public string SenderEmail { get; set; }
        public string SenderPassword { get; set; }
    }
}