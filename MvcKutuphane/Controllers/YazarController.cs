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
    public class YazarController : Controller
    {
        DBLIBRARYEntities db = new DBLIBRARYEntities();
        // GET: Yazar
        public ActionResult Index(string search, int page = 1)
        {
            var yz = from x in db.TBLYAZAR select x;
            if (!string.IsNullOrEmpty(search))
            {
                yz = yz.Where(x => x.DURUM == true && x.AD.ToUpper().Contains(search.ToUpper()) || x.SOYAD.ToUpper().Contains(search.ToUpper()));
            }
            return View(yz.Where(x=>x.DURUM==true).ToList().ToPagedList(page, 10));
        }

        public ActionResult PasifYazar(string search, int page = 1)
        {
            var yz = from x in db.TBLYAZAR select x;
            if (!string.IsNullOrEmpty(search))
            {
                yz = yz.Where(x => x.DURUM == false && x.AD.ToUpper().Contains(search.ToUpper()) || x.SOYAD.ToUpper().Contains(search.ToUpper()));
            }
            return View(yz.Where(x => x.DURUM == false).ToList().ToPagedList(page, 10));
        }
        public ActionResult AktifEt(int id)
        {
            var ktg = db.TBLYAZAR.Find(id);
            ktg.DURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult YazarEkle()
        {
            TBLYAZAR y = new TBLYAZAR();
            return View(y);
        }
        [HttpPost]
        public ActionResult YazarEkle(TBLYAZAR y)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            db.TBLYAZAR.Add(y);
            y.DURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult YazarGetir(int id)
        {
            var yz = db.TBLYAZAR.Find(id);
            return View(yz);
        }

        public ActionResult YazarGuncelle(TBLYAZAR y)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("YazarGetir/" + y.ID);
            }
            var yz = db.TBLYAZAR.Find(y.ID);
            yz.AD = y.AD;
            yz.SOYAD = y.SOYAD;
            yz.DETAY = y.DETAY;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult YazarSil(int id)
        {
            var yz = db.TBLYAZAR.Find(id);
            yz.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult YazarKitap(int id,string search, int page = 1)
        {
            var yzrad = db.TBLYAZAR.Where(e => e.ID == id).Select(s => s.AD + " " + s.SOYAD).FirstOrDefault();
            ViewBag.y1 = yzrad;
            var yz = from x in db.TBLKITAP select x;
            if (!string.IsNullOrEmpty(search))
            {
                yz = yz.Where(x => x.DURUM == true && x.YAZAR==id && x.AD.ToUpper().Contains(search.ToUpper()));
            }
            return View(yz.Where(x => x.DURUM == true && x.YAZAR == id).ToList().ToPagedList(page, 10));
        }
    }
}