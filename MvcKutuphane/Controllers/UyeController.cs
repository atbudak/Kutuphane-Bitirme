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
    public class UyeController : Controller
    {
        DBLIBRARYEntities db = new DBLIBRARYEntities();
        // GET: Uye
        public ActionResult Index(string search, int page = 1)
        {
            var uye = from x in db.TBLUYELER select x;
            if (!string.IsNullOrEmpty(search))
            {
                uye = uye.Where(x => x.AD.ToUpper().Contains(search.ToUpper()) || x.SOYAD.ToUpper().Contains(search.ToUpper()));
            }
            return View(uye.ToList().ToPagedList(page, 10));
        }
        [HttpGet]
        public ActionResult UyeEkle()
        {
            TBLUYELER u = new TBLUYELER();
            return View(u);
        }
        [HttpPost]
        public ActionResult UyeEkle(TBLUYELER u)
        {
            db.TBLUYELER.Add(u);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UyeSil(int id)
        {
            var uye = db.TBLUYELER.Find(id);
            db.TBLUYELER.Remove(uye);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UyeGetir(int id)
        {
            var uye = db.TBLUYELER.Find(id);
            return View("UyeGetir", uye);
        }

        public ActionResult UyeGuncelle(TBLUYELER p)
        {
            var uye = db.TBLUYELER.Find(p.ID);
            uye.AD = p.AD;
            uye.SOYAD = p.SOYAD;
            uye.SIFRE = p.SIFRE;
            uye.KULLANICIADI = p.KULLANICIADI;
            uye.MAIL = p.MAIL;
            uye.FOTOGRAF = p.FOTOGRAF;
            uye.TELEFON = p.TELEFON;
            uye.OKUL = p.OKUL;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}