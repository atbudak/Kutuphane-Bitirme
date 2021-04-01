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
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}