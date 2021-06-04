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
    public class DuyuruController : Controller
    {
        DBLIBRARYEntities db = new DBLIBRARYEntities();
        // GET: Duyuru
        public ActionResult Index(string search, int page = 1)
        {
            var ara = from x in db.TBLDUYURULAR select x;
            if (!string.IsNullOrEmpty(search))
            {
                ara = ara.Where(x => x.DURUM==true && x.KATEGORI.ToUpper().Contains(search.ToUpper()));
            }
            return View(ara.Where(x => x.DURUM == true).ToList().ToPagedList(page, 10));
        }
        [HttpGet]
        public ActionResult YeniDuyuru()
        {
            TBLDUYURULAR dy = new TBLDUYURULAR();
            return View(dy);
        }
        [HttpPost]
        public ActionResult YeniDuyuru(TBLDUYURULAR d)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            db.TBLDUYURULAR.Add(d);
            d.DURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DuyuruSil(int id)
        {
            var duyuru = db.TBLDUYURULAR.Find(id);
            duyuru.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DuyuruDetay(int id)
        {
            var detay = db.TBLDUYURULAR.Find(id);
            return View("DuyuruDetay", detay);
        }

        public ActionResult DuyuruGuncelle(TBLDUYURULAR d)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("DuyuruDetay/" + d.ID);
            }
            var duyuru = db.TBLDUYURULAR.Find(d.ID);
            duyuru.TARIH = d.TARIH;
            duyuru.ICERIK = d.ICERIK;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}