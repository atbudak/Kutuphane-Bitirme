using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.EntityFramework;
using MvcKutuphane.Models.Classes;

namespace MvcKutuphane.Controllers
{   [AllowAnonymous]
    public class VitrinController : Controller
    {
        DBLIBRARYEntities db = new DBLIBRARYEntities();
        // GET: Vitrin
        [HttpGet]
        public ActionResult Index()
        {
            Class1 cs = new Class1();
            cs.Kitap1 = db.TBLKITAP.ToList();
            cs.hakkimizda1 = db.TBLHAKKIMIZDA.ToList();
            cs.slider1 = db.TBLRESIMLER.ToList();
            return View(cs);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(TBLILETISIM t)
        {
            var tr = db.TBLILETISIM.Add(t);
            tr.DURUM = true;
            TempData["mesaj"] = "Mesajınız bize ulaştı. En yakın zamanda size dönüş sağlayacağız.";
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Galeri(string search)
        {
            var ktp = from x in db.TBLKITAP select x;
            if (!string.IsNullOrEmpty(search))
            {
                ktp.Where(x => x.AD.ToUpper().Contains(search.ToUpper()));
            }            
            return View(ktp.ToList());
        }
    }
}