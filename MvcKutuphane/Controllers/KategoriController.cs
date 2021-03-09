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
                ara = ara.Where(x => x.AD.ToUpper().Contains(search.ToUpper()));
            }
            return View(ara.ToList().ToPagedList(page,10));
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
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriSil(int id)
        {
            var ktg = db.TBLKATEGORI.Find(id);
            db.TBLKATEGORI.Remove(ktg);
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