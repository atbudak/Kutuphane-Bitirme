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
    public class BizeUlasinController : Controller
    {
        DBLIBRARYEntities db = new DBLIBRARYEntities();
        // GET: BizeUlasin
        public ActionResult Index(string search, int page = 1)
        {
            var ara = from x in db.TBLILETISIM select x;
            if (!string.IsNullOrEmpty(search))
            {
                ara = ara.Where(x => x.DURUM==true && x.AD.ToUpper().Contains(search.ToUpper()) || x.SOYAD.ToUpper().Contains(search.ToUpper()));
            }
            return View(ara.ToList().ToPagedList(page, 10));
        }
        public ActionResult TumMesajlar(string search, int page = 1)
        {
            var ara = from x in db.TBLILETISIM select x;
            if (!string.IsNullOrEmpty(search))
            {
                ara = ara.Where(x => x.AD.ToUpper().Contains(search.ToUpper()) || x.SOYAD.ToUpper().Contains(search.ToUpper()));
            }
            return View(ara.ToList().ToPagedList(page, 10));
        }
        public ActionResult MesajSil(int id)
        {
            var sil = db.TBLILETISIM.Find(id);
            sil.DURUM = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MesajGetir(int id)
        {
            var ms = db.TBLILETISIM.Find(id);
            return View("MesajGetir", ms);
        }
    }
}