using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Indelible.Models
{
    public class DocumentEmail
    {
        public Document Document { get; set; }
        public Email Email { get; set; }
    }
}