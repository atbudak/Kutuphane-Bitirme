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
                uye = uye.Where(x => x.DURUM == true && x.AD.ToUpper().Contains(search.ToUpper()) || x.SOYAD.ToUpper().Contains(search.ToUpper()));
            }
            return View(uye.Where(x=>x.DURUM==true).ToList().ToPagedList(page, 10));
        }
        public ActionResult PasifUye(string search, int page = 1)
        {
            var uye = from x in db.TBLUYELER select x;
            if (!string.IsNullOrEmpty(search))
            {
                uye = uye.Where(x => x.DURUM == false && x.AD.ToUpper().Contains(search.ToUpper()) || x.SOYAD.ToUpper().Contains(search.ToUpper()));
            }
            return View(uye.Where(X => X.DURUM == false).ToList().ToPagedList(page, 10));
        }
        public ActionResult AktifEt(int id)
        {
            var ktg = db.TBLUYELER.Find(id);
            ktg.DURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");
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
            u.DURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UyeSil(int id)
        {
            var uye = db.TBLUYELER.Find(id);
            uye.DURUM = false;
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
        public ActionResult UyeKitapGecmisi(int id, string search, int page = 1)
        {
            var uyeadi = db.TBLUYELER.Where(e => e.ID == id).Select(s => s.AD + " " + s.SOYAD).FirstOrDefault();
            ViewBag.uye1 = uyeadi;
            var uye = from x in db.TBLHAREKET select x;
            if (!string.IsNullOrEmpty(search))
            {
                uye = uye.Where(x => x.TBLUYELER.DURUM == true && x.TBLKITAP.AD.ToUpper().Contains(search.ToUpper()) || x.TBLPERSONEL.PERSONEL.ToUpper().Contains(search.ToUpper()));
            }
            return View(uye.Where(x => x.TBLUYELER.DURUM == true && x.UYE==id).ToList().ToPagedList(page, 10));
        }
    }
}