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
    public class PersonelController : Controller
    {
        DBLIBRARYEntities db = new DBLIBRARYEntities();
        // GET: Personel
        public ActionResult Index(string search,int page = 1)
        {
            var kt = from x in db.TBLPERSONEL select x;
            if (!string.IsNullOrEmpty(search))
            {
                kt = kt.Where(x => x.PERSONEL.ToUpper().Contains(search.ToUpper()));
            }
            return View(kt.ToList().ToPagedList(page, 10));
        }
        [HttpGet]
        public ActionResult PersonelEkle()
        {
            TBLPERSONEL p = new TBLPERSONEL();
            return View(p);
        }
        [HttpPost]
        public ActionResult PersonelEkle(TBLPERSONEL p)
        {
            db.TBLPERSONEL.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelGetir(int id)
        {
            var per = db.TBLPERSONEL.Find(id);
            return View("PersonelGetir",per);
        }

        public ActionResult PersonelGuncelle(TBLPERSONEL p)
        {
            var per = db.TBLPERSONEL.Find(p.ID);
            per.PERSONEL = p.PERSONEL;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelSil(int id)
        {
            var per = db.TBLPERSONEL.Find(id);
            db.TBLPERSONEL.Remove(per);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}