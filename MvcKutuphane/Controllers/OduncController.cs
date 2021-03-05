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
    public class OduncController : Controller
    {
        DBLIBRARYEntities db = new DBLIBRARYEntities();
        // GET: Odunc
        public ActionResult Index(string search, int page = 1)
        {
            var ktp = from x in db.TBLHAREKET select x;
            if (!string.IsNullOrEmpty(search))
            {
                ktp = ktp.Where(x => x.TBLUYELER.AD.ToUpper().Contains(search.ToUpper()) || x.TBLUYELER.SOYAD.ToUpper().Contains(search.ToUpper()) || x.TBLKITAP.AD.ToUpper().Contains(search.ToUpper()));
            }
            return View(ktp.Where(x=>x.ISLEMDURUM==false).ToList().ToPagedList(page, 10));
        }
        [HttpGet]
        public ActionResult OduncVer()
        {
            //List<SelectListItem> per = (from x in db.TBLPERSONEL.ToList() select new  SelectListItem{ Text=x.PERSONEL,Value=x.ID.ToString() }).ToList();
            //ViewBag.per1 = per;
            return View();
        }
        [HttpPost]
        public ActionResult OduncVer(TBLHAREKET h)  
        {
            //var per = db.TBLPERSONEL.Where(x => x.ID == h.TBLPERSONEL.ID).FirstOrDefault();
            //h.TBLPERSONEL = per;
            h.ISLEMDURUM = false;
            db.TBLHAREKET.Add(h);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult OduncIade(int id)
        {
            var ts = db.TBLHAREKET.Find(id);
            return View("OduncIade",ts);
        }
        public ActionResult OduncGuncelle(TBLHAREKET h)
        {
            var hr = db.TBLHAREKET.Find(h.ID);
            hr.UYEGETIRTARIH = h.UYEGETIRTARIH;
            hr.ISLEMDURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}