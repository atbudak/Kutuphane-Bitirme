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
    public class KategoriController : Controller
    {
        DBLIBRARYEntities db = new DBLIBRARYEntities();
        // GET: Kategori
        public ActionResult Index(string search, int page = 1)
        {
            var ara = from x in db.TBLKATEGORI select x;
            if (!string.IsNullOrEmpty(search))
            {
                ara = ara.Where(x => x.AD.ToUpper().Contains(search.ToUpper()) && x.DURUM==true);
            }
            return View(ara.Where(X=>X.DURUM==true).ToList().ToPagedList(page,10));
        }
        public ActionResult PasifKategori(string search, int page = 1)
        {
            var ara = from x in db.TBLKATEGORI select x;
            if (!string.IsNullOrEmpty(search))
            {
                ara = ara.Where(x => x.AD.ToUpper().Contains(search.ToUpper()) && x.DURUM == false);
            }
            return View(ara.Where(X => X.DURUM == false).ToList().ToPagedList(page, 10));
        }
        public ActionResult AktifEt(int id)
        {
            var ktg = db.TBLKATEGORI.Find(id);
            ktg.DURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult KategoriEkle()
        {
            TBLKATEGORI t = new TBLKATEGORI();
            return View(t);
        }
        [HttpPost]
        public ActionResult KategoriEkle(TBLKATEGORI p)
        {
            db.TBLKATEGORI.Add(p);
            p.DURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriSil(int id)
        {
            var ktg = db.TBLKATEGORI.Find(id);
            ktg.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {
            var ktg = db.TBLKATEGORI.Find(id);
            return View(ktg);
        }
        public ActionResult KategoriGuncelle(TBLKATEGORI k)
        {
            var kt = db.TBLKATEGORI.Find(k.ID);
            kt.AD = k.AD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}