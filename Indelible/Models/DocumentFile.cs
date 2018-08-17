using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Indelible.Models
{
    public class DocumentFile
    {
        public Document Document { get; set; }
        [DataType(DataType.Upload)]
        public HttpPostedFileBase File { get; set; }
    }
}