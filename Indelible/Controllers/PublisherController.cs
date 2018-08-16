using Indelible.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Indelible.Controllers
{
    public class PublisherController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Publisher
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            List<Document> documents = db.Documents.Where(d => d.UserId == userId).ToList();

            return View(documents);
        }
    }
}