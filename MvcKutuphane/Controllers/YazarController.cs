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
                yz = yz.Where(x => x.AD.ToUpper().Contains(search.ToUpper()) || x.SOYAD.ToUpper().Contains(search.ToUpper()));
            }
            return View(yz.ToList().ToPagedList(page, 10));
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
            db.TBLYAZAR.Add(y);
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
            db.TBLYAZAR.Remove(yz);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}