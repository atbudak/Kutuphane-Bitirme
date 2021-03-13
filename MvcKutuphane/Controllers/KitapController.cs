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
    public class KitapController : Controller
    {
        DBLIBRARYEntities db = new DBLIBRARYEntities();
        // GET: Kitap
        public ActionResult Index(string search,int page = 1)
        {            
            var ktp = from x in db.TBLKITAP select x;
            if (!string.IsNullOrEmpty(search))
            {
                ktp = ktp.Where(x => x.AD.ToUpper().Contains(search.ToUpper()) || x.TBLYAZAR.AD.ToUpper().Contains(search.ToUpper()) || x.TBLYAZAR.SOYAD.ToUpper().Contains(search.ToUpper()));
            }
            return View(ktp.ToList().ToPagedList(page,10));
        }

        [HttpGet]
        public ActionResult KitapEkle()
        {
            List<SelectListItem> kt = (from x in db.TBLKATEGORI.ToList()
                                       select new SelectListItem { Text = x.AD, Value = x.ID.ToString() }).ToList();
            ViewBag.ktg1 = kt;

            List<SelectListItem> yz = (from x in db.TBLYAZAR.ToList()
                                       select new SelectListItem { Text = x.AD + " " + x.SOYAD, Value = x.ID.ToString() }).ToList();
            ViewBag.yz1 = yz;
            TBLKITAP k = new TBLKITAP();
            return View(k);
        }
        [HttpPost]
        public ActionResult KitapEkle(TBLKITAP k)
        {
            var ktg = db.TBLKATEGORI.Where(x => x.ID == k.TBLKATEGORI.ID).FirstOrDefault();
            var yz = db.TBLYAZAR.Where(x => x.ID == k.TBLYAZAR.ID).FirstOrDefault();
            k.DURUM = true;
            k.TBLKATEGORI = ktg;
            k.TBLYAZAR = yz;
            db.TBLKITAP.Add(k);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KitapSil(int id)
        {
            var kt = db.TBLKITAP.Find(id);
            db.TBLKITAP.Remove(kt);
            db.SaveChanges(); 
            return RedirectToAction("Index");
        }
        public ActionResult KitapGetir(int id)
        {
            List<SelectListItem> ktgr = (from x in db.TBLKATEGORI.ToList() select new SelectListItem { Text = x.AD, Value = x.ID.ToString() }).ToList();
            List<SelectListItem> yz = (from x in db.TBLYAZAR.ToList() select new SelectListItem { Text = x.AD + " " + x.SOYAD, Value = x.ID.ToString() }).ToList();
            ViewBag.ktg2 = ktgr;
            ViewBag.yz2 = yz;
            var bkg = db.TBLKITAP.Find(id);
            return View("KitapGetir",bkg);
        }
        public ActionResult KitapGuncelle(TBLKITAP k)
        {
            var bk = db.TBLKITAP.Find(k.ID);
            bk.AD = k.AD;
            bk.SAYFA = k.SAYFA;
            bk.YAYINEVI = k.YAYINEVI;
            bk.BASIMYIL = k.BASIMYIL;
            var kt = db.TBLKATEGORI.Where(x => x.ID == k.TBLKATEGORI.ID).FirstOrDefault();
            var yz = db.TBLYAZAR.Where(x => x.ID == k.TBLYAZAR.ID).FirstOrDefault();
            bk.KATEGORI = kt.ID;
            bk.YAZAR = yz.ID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
      
    }
}