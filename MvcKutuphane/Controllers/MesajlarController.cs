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
    [Authorize(Roles = "Admin,User")]
    public class MesajlarController : Controller
    {
        DBLIBRARYEntities db = new DBLIBRARYEntities();

        // GET: Mesajlar        
        public ActionResult Index(string search, int page = 1)
        {
            var ktp = from x in db.TBLMESAJ select x;
            var uyemail = (string)Session["Mail"];
            if (!string.IsNullOrEmpty(search))
            {
                ktp = ktp.Where(x => x.GONDEREN.ToUpper().Contains(search.ToUpper()) && x.ALICI == uyemail);
            }
            return View(ktp.Where(x => x.ALICI == uyemail && x.G_DURUM != "0" && x.G_DURUM != "2").ToList().ToPagedList(page, 25));
        }


        public ActionResult Giden(string search, int page = 1)
        {
            var ktp = from x in db.TBLMESAJ select x;
            var uyemail = (string)Session["Mail"];
            if (!string.IsNullOrEmpty(search))
            {
                ktp = ktp.Where(x => x.ALICI.ToUpper().Contains(search.ToUpper()) && x.GONDEREN == uyemail);
            }
            return View(ktp.Where(x => x.GONDEREN == uyemail && x.A_DURUM != "0" && x.A_DURUM != "2").ToList().ToPagedList(page, 25));
        }
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            TBLMESAJ m = new TBLMESAJ();
            return View(m);
        }
        [HttpPost]
        public ActionResult YeniMesaj(TBLMESAJ m)
        {
            DateTime date = DateTime.Parse(DateTime.Now.ToShortDateString());
            var uyemail = (string)Session["Mail"];
            db.TBLMESAJ.Add(m);
            m.GONDEREN = uyemail.ToString();
            m.TARIH = date;
            db.SaveChanges();
            return RedirectToAction("Giden", "Mesajlar");
        }

        public PartialViewResult Partial1()
        {
            var uyemail = (string)Session["Mail"];
            var gelensayisi = db.TBLMESAJ.Where(x => x.ALICI == uyemail).Count();
            var gidensayisi = db.TBLMESAJ.Where(x => x.GONDEREN == uyemail).Count();

            var trash1 = db.TBLMESAJ.Where(x => x.GONDEREN == uyemail && x.A_DURUM == "0").Count();
            var trash2 = db.TBLMESAJ.Where(x => x.ALICI == uyemail && x.G_DURUM == "0").Count();

            var taslak1 = db.TBLMESAJ.Where(x => x.ALICI == uyemail && x.G_DURUM == "2").Count();
            var taslak2 = db.TBLMESAJ.Where(x => x.GONDEREN == uyemail && x.A_DURUM == "2").Count();

            var onemsiz1 = db.TBLMESAJ.Where(x => x.ALICI == uyemail && x.G_DURUM == "3" || x.G_DURUM == null).Count();
            var onemsiz2 = db.TBLMESAJ.Where(x => x.GONDEREN == uyemail && x.A_DURUM == "3" || x.A_DURUM == null).Count();

            ViewBag.g1 = gelensayisi - trash2 - taslak1;
            ViewBag.g2 = gidensayisi - trash1 - taslak2;
            ViewBag.g3 = trash1 + trash2;
            ViewBag.g4 = taslak1 + taslak2;
            ViewBag.g5 = onemsiz1 + onemsiz2;
            return PartialView();
        }
        public ActionResult MesajGoruntule(int id)
        {
            var msj = db.TBLMESAJ.Find(id);
            return View(msj);
        }

        public ActionResult Trash(TBLMESAJ t)
        {
            var uyemail = (string)Session["Mail"];

            var ms = db.TBLMESAJ.Find(t.ID);
            if (ms.ALICI == uyemail)
            {
                //çöp kutusu numarası '0' dır
                ms.G_DURUM = "0";
                db.SaveChanges();
                return RedirectToAction("Index", "Mesajlar");
            }
            else
            {
                ms.A_DURUM = "0";
                db.SaveChanges();
                return RedirectToAction("Giden", "Mesajlar");
            }
        }

        public ActionResult Important(TBLMESAJ t)
        {
            var uyemail = (string)Session["Mail"];

            var ms = db.TBLMESAJ.Find(t.ID);
            if (ms.ALICI == uyemail)
            {
                //Önemli numarası '1' dır. 
                //Önemliden çıkarma numarası '3' tür.
                if (ms.G_DURUM == "1")
                { ms.G_DURUM = "3"; }
                else { ms.G_DURUM = "1"; }
                db.SaveChanges();
                return RedirectToAction("Index", "Mesajlar");
            }
            else
            {
                if (ms.A_DURUM == "1")
                { ms.A_DURUM = "3"; }
                else { ms.A_DURUM = "1"; }
                db.SaveChanges();
                return RedirectToAction("Giden", "Mesajlar");
            }
        }
        public ActionResult Taslak(TBLMESAJ t)
        {
            var uyemail = (string)Session["Mail"];

            var ms = db.TBLMESAJ.Find(t.ID);
            if (ms.ALICI == uyemail)
            {
                //Taslak numarası '2' dır
                ms.G_DURUM = "2";
                db.SaveChanges();
                return RedirectToAction("Index", "Mesajlar");
            }
            else
            {
                ms.A_DURUM = "2";
                db.SaveChanges();
                return RedirectToAction("Giden", "Mesajlar");
            }
        }
        public ActionResult GeriAl(TBLMESAJ t)
        {
            var uyemail = (string)Session["Mail"];

            var ms = db.TBLMESAJ.Find(t.ID);
            if (ms.ALICI == uyemail)
            {
                //Normal durum numarası '3' dır
                ms.G_DURUM = "3";
                db.SaveChanges();
                return RedirectToAction("Index", "Mesajlar");
            }
            else
            {
                ms.A_DURUM = "3";
                db.SaveChanges();
                return RedirectToAction("Giden", "Mesajlar");
            }
        }

        public ActionResult CopKutusuGetir(string search, int page = 1)
        {
            var ktp = from x in db.TBLMESAJ select x;
            var uyemail = (string)Session["Mail"];
            if (!string.IsNullOrEmpty(search))
            {
                ktp = ktp.Where(x => x.GONDEREN.ToUpper().Contains(search.ToUpper()) || x.ALICI.ToUpper().Contains(search.ToUpper()));
            }
            return View(ktp.Where(x => (x.ALICI == uyemail || x.GONDEREN == uyemail) && (x.G_DURUM == "0" || x.A_DURUM == "0")).ToList().ToPagedList(page, 25));
        }

        public ActionResult TaslakGetir(string search, int page = 1)
        {
            var ktp = from x in db.TBLMESAJ select x;
            var uyemail = (string)Session["Mail"];
            if (!string.IsNullOrEmpty(search))
            {
                ktp = ktp.Where(x => x.GONDEREN.ToUpper().Contains(search.ToUpper()) || x.ALICI.ToUpper().Contains(search.ToUpper()));
            }
            return View(ktp.Where(x => (x.ALICI == uyemail || x.GONDEREN == uyemail) && (x.G_DURUM == "2" || x.A_DURUM == "2")).ToList().ToPagedList(page, 25));
        }

        public ActionResult OnemliGetir(string search, int page = 1)
        {
            var ktp = from x in db.TBLMESAJ select x;
            var uyemail = (string)Session["Mail"];
            if (!string.IsNullOrEmpty(search))
            {
                ktp = ktp.Where(x => x.GONDEREN.ToUpper().Contains(search.ToUpper()) || x.ALICI.ToUpper().Contains(search.ToUpper()));
            }
            return View(ktp.Where(x => (x.ALICI == uyemail || x.GONDEREN == uyemail) && (x.G_DURUM == "1" || x.A_DURUM == "1")).ToList().ToPagedList(page, 25));
        }

    }
}