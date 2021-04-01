using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.EntityFramework;
using PagedList;
using PagedList.Mvc;

namespace MvcKutuphane.Controllers
{
    [Authorize]
    public class MesajlarController : Controller
    {
        DBLIBRARYEntities db = new DBLIBRARYEntities();
        // GET: Mesajlar
        public ActionResult Index(string search, int page = 1)
        {
            var ktp = from x in db.TBLMESAJ select x;
            var uyemail = (string)Session["Mail"];
            if (!string.IsNullOrEmpty(search))
            {
                ktp = ktp.Where(x => x.GONDEREN.ToUpper().Contains(search.ToUpper()) && x.ALICI == uyemail);
            }
            return View(ktp.Where(x => x.ALICI == uyemail).ToList().ToPagedList(page, 10));
        }
        public ActionResult Giden(string search, int page = 1)
        {
            var ktp = from x in db.TBLMESAJ select x;
            var uyemail = (string)Session["Mail"];
            if (!string.IsNullOrEmpty(search))
            {
                ktp = ktp.Where(x => x.ALICI.ToUpper().Contains(search.ToUpper()) && x.GONDEREN == uyemail);
            }
            return View(ktp.Where(x => x.GONDEREN == uyemail).ToList().ToPagedList(page, 10));
        }
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            TBLMESAJ m = new TBLMESAJ();
            return View(m);
        }
        [HttpPost]
        public ActionResult YeniMesaj(TBLMESAJ m)
        {
            DateTime date =DateTime.Parse(DateTime.Now.ToShortDateString());
            var uyemail = (string)Session["Mail"];
            db.TBLMESAJ.Add(m);
            m.GONDEREN = uyemail.ToString();
            m.TARIH = date;
            db.SaveChanges();
            return RedirectToAction("Giden","Mesajlar");
        }

        public PartialViewResult Partial1()
        {
            var uyemail = (string)Session["Mail"];
            var gelensayisi = db.TBLMESAJ.Where(x => x.ALICI == uyemail).Count();
            var gidensayisi = db.TBLMESAJ.Where(x => x.GONDEREN == uyemail).Count();
            ViewBag.g1 = gelensayisi;
            ViewBag.g2 = gidensayisi;
            return PartialView();
        }
    }
}