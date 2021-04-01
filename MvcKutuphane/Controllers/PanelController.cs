using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.EntityFramework;
using PagedList;
using PagedList.Mvc;
using System.Web.Security;

namespace MvcKutuphane.Controllers
{
    public class PanelController : Controller
    {
        DBLIBRARYEntities db = new DBLIBRARYEntities();
        // GET: Panel
        public ActionResult Index()
        {
            var uyemail = (string)Session["Mail"];
            var degerler = db.TBLUYELER.FirstOrDefault(x => x.MAIL == uyemail);
            return View(degerler);
        }
        public ActionResult Profil()
        {
            var uyemail = (string)Session["Mail"];
            var degerler = db.TBLUYELER.FirstOrDefault(x => x.MAIL == uyemail);
            var d1 = db.TBLUYELER.Where(x => x.MAIL == degerler.MAIL).Select(y => y.AD).FirstOrDefault();
            ViewBag.dg1 = d1;
            var d2 = db.TBLUYELER.Where(x => x.MAIL == degerler.MAIL).Select(y => y.SOYAD).FirstOrDefault();
            ViewBag.dg2 = d2;
            var d3 = db.TBLUYELER.Where(x => x.MAIL == degerler.MAIL).Select(y => y.OKUL).FirstOrDefault();
            ViewBag.dg3 = d3;
            var d4 = db.TBLUYELER.Where(x => x.MAIL == degerler.MAIL).Select(y => y.TELEFON).FirstOrDefault();
            ViewBag.dg4 = d4;
            var d5 = db.TBLUYELER.Where(x => x.MAIL == degerler.MAIL).Select(y => y.KULLANICIADI).FirstOrDefault();
            ViewBag.dg5 = d5;
            var d6 = db.TBLUYELER.Where(x => x.MAIL == degerler.MAIL).Select(y => y.MAIL).FirstOrDefault();
            ViewBag.dg6 = d6;
            var d7 = db.TBLUYELER.Where(x => x.MAIL == degerler.MAIL).Select(y => y.FOTOGRAF).FirstOrDefault();
            ViewBag.dg7 = d7;

            var uyeid = db.TBLUYELER.Where(x => x.MAIL == degerler.MAIL).Select(y => y.ID).FirstOrDefault();
            var d8 = db.TBLHAREKET.Where(x => x.UYE == uyeid).Count();
            ViewBag.dg8 = d8;
            var d9 = db.TBLMESAJ.Where(x => x.ALICI == degerler.MAIL).Count();
            ViewBag.dg9 = d9;
            var d10 = db.TBLDUYURULAR.Count();
            ViewBag.dg10 = d10;
            return View(degerler);
        }

        [HttpPost]
        public ActionResult Index2(TBLUYELER t)
        {
            var kullanici = (string)Session["Mail"];
            var uye = db.TBLUYELER.FirstOrDefault(x => x.MAIL == kullanici);
            uye.SIFRE = t.SIFRE;
            uye.AD = t.AD;
            uye.SOYAD = t.SOYAD;
            uye.KULLANICIADI = t.KULLANICIADI;
            uye.OKUL = t.OKUL;
            uye.FOTOGRAF = t.FOTOGRAF;
            uye.TELEFON = t.TELEFON;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Kitaplarim(string search, int page = 1)
        {
            var kullanici = (string)Session["Mail"];
            var ktp = from x in db.TBLHAREKET select x;
            if (!string.IsNullOrEmpty(search))
            {
                ktp = ktp.Where(x =>  x.TBLKITAP.AD.ToUpper().Contains(search.ToUpper()) && x.TBLUYELER.MAIL == kullanici);
            }
            return View(ktp.Where(x => x.TBLUYELER.MAIL == kullanici).ToList().ToPagedList(page, 10));
        }

        public ActionResult Duyurular(string search, int page = 1)
        {
            var ktp = from x in db.TBLDUYURULAR select x;
            if (!string.IsNullOrEmpty(search))
            {
                ktp = ktp.Where(x => x.DURUM == true && x.KATEGORI.ToUpper().Contains(search.ToUpper()));
            }
            return View(ktp.Where(x => x.DURUM == true).OrderByDescending(x=>x.TARIH).ToList().ToPagedList(page, 10));
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap", "Login");
        }

        public PartialViewResult Partial1()
        {
            var deger = db.TBLDUYURULAR.ToList();
            return PartialView("Partial1",deger);
        }
    }
}