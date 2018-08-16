using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Indelible.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        public string Hash { get; set; }
        public string Title { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string UserName { get; set; }
    }
}