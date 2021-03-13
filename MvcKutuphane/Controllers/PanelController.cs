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
    [Authorize]
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
            return View(ktp.Where(x => x.DURUM == true).ToList().ToPagedList(page, 10));
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap", "Login");
        }
    }
}