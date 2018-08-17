using Indelible.Models;
using Ipfs;
using System.IO;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Threading.Tasks;

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

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Upload([Bind(Include =("Id,Title"))] Document document)
        {
            Document newDocument = new Document { Id = document.Id, Title = document.Title };

            string filepath = "test.txt";
            MemoryStream mStream = new MemoryStream(Encoding.UTF8.GetBytes(filepath));
            IpfsStream file = new IpfsStream(filepath, mStream);
            IpfsClient ipfs = new IpfsClient();
            MerkleNode node = await ipfs.Add(file);

            newDocument.Hash = node.Hash.ToString();
            newDocument.UserId = User.Identity.GetUserId();
            newDocument.UserName = db.Users.Where(u => u.Id == newDocument.UserId).FirstOrDefault().UserName;

            db.Documents.Add(newDocument);
            db.SaveChanges();

            return View();
        }
    }
}