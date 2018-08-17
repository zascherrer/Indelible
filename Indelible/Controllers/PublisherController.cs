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
            DocumentFile documentFile = new DocumentFile();

            return View(documentFile);
        }

        [HttpPost]
        public async Task<ActionResult> Upload([Bind(Include =("Id,Title"))] Document document, HttpPostedFileBase file)
        {
            Document newDocument = new Document { Id = document.Id, Title = document.Title };

            string filepath = System.IO.Path.GetFullPath(file.FileName);
            MemoryStream mStream = new MemoryStream(Encoding.UTF8.GetBytes(filepath));
            IpfsStream fileIpfs = new IpfsStream(filepath, mStream);
            IpfsClient ipfs = new IpfsClient();
            MerkleNode node = await ipfs.Add(fileIpfs);

            newDocument.Hash = node.Hash.ToString();
            newDocument.UserId = User.Identity.GetUserId();
            newDocument.UserName = db.Users.Where(u => u.Id == newDocument.UserId).FirstOrDefault().UserName;

            db.Documents.Add(newDocument);
            db.SaveChanges();

            return View();
        }

        //[HttpPost]
        //public ActionResult EditBanner1(HomeInfo info, HttpPostedFileBase file1)
        //{
        //    var homePage = db.homeInfos.Select(h => h).FirstOrDefault();
        //    if (file1 != null)
        //    {
        //        string pic = System.IO.Path.GetFileName(file1.FileName);
        //        string path = System.IO.Path.Combine(
        //                               Server.MapPath("~/Content"), pic);
        //        file1.SaveAs(path);
        //        homePage.SliderPic1 = "/Content/" + pic;
        //    }
        //    db.SaveChanges();
        //    return RedirectToAction("Upload");

        //}
    }
}