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
                kt = kt.Where(x => x.DURUM==true && x.PERSONEL.ToUpper().Contains(search.ToUpper()));
            }
            return View(kt.Where(x => x.DURUM == true).ToList().ToPagedList(page, 10));
        }
        public ActionResult PasifPersonel(string search, int page = 1)
        {
            var kt = from x in db.TBLPERSONEL select x;
            if (!string.IsNullOrEmpty(search))
            {
                kt = kt.Where(x => x.DURUM == false && x.PERSONEL.ToUpper().Contains(search.ToUpper()));
            }
            return View(kt.Where(x => x.DURUM == false).ToList().ToPagedList(page, 10));
        }
        public ActionResult AktifEt(int id)
        {
            var ktg = db.TBLPERSONEL.Find(id);
            ktg.DURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");
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
            if (!ModelState.IsValid)
            {
                return View();
            }
            db.TBLPERSONEL.Add(p);
            p.DURUM = true;
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
            if (!ModelState.IsValid)
            {
                return RedirectToAction("PersonelGetir/" + p.ID);
            }
            var per = db.TBLPERSONEL.Find(p.ID);
            per.PERSONEL = p.PERSONEL;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelSil(int id)
        {
            var per = db.TBLPERSONEL.Find(id);
            per.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}