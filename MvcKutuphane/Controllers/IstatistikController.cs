using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.EntityFramework;
using System.IO;
using PagedList;
using PagedList.Mvc;

namespace MvcKutuphane.Controllers
{
    public class IstatistikController : Controller
    {
        DBLIBRARYEntities db = new DBLIBRARYEntities();
        // GET: Istatistik
        public ActionResult Index()
        {
            var deger1 = db.TBLUYELER.Count();
            var deger2 = db.TBLKITAP.Count();
            var deger3 = db.TBLKITAP.Where(x => x.DURUM == false).Count();
            var deger4 = db.TBLCEZALAR.Sum(x => x.PARA);
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            ViewBag.dgr4 = deger4;
            return View();
        }

        public ActionResult Hava()
        {
            return View();
        }
        public ActionResult SliderResimYukle()
        {
            var res = db.TBLRESIMLER.ToList();
            return View(res);
        }
        [HttpPost]
        public ActionResult Resimyukle(HttpPostedFileBase RESIMYOLU, TBLRESIMLER r)
        {
            if(RESIMYOLU.ContentLength > 0)
            {
                string dosyayolu = Path.Combine(Server.MapPath("~/web/slider_img/"), Path.GetFileName(RESIMYOLU.FileName));
                RESIMYOLU.SaveAs(dosyayolu);
                r.RESIMYOLU = RESIMYOLU.FileName;
                db.TBLRESIMLER.Add(r);
                db.SaveChanges();
                return RedirectToAction("SliderResimYukle");
            }
            else { return RedirectToAction("SliderResimYukle"); }
            
        }

        public ActionResult ResimGoruntule(string search, int page = 1)
        {
            var ara = from x in db.TBLRESIMLER select x;
            if (!string.IsNullOrEmpty(search))
            {
                ara = ara.Where(x => x.RESIMYOLU.ToUpper().Contains(search.ToUpper()));
            }
            return View(ara.ToList().ToPagedList(page, 10));
        }

        public ActionResult ResimGetir(int id)
        {
            var rs = db.TBLRESIMLER.Find(id);
            return View("ResimGetir",rs);
        }
        public ActionResult ResimGuncelle(TBLRESIMLER p)
        {
            var rs = db.TBLRESIMLER.Find(p.ID);
            rs.RESIMDETAY = p.RESIMDETAY;
            rs.DURUM = p.DURUM;
            db.SaveChanges();
            return RedirectToAction("ResimGoruntule");
        }

        public ActionResult ResimYoluSil(int id)
        {
            var res = db.TBLRESIMLER.Find(id);
            string FileName = res.RESIMYOLU;
            string dosyayolu = Path.Combine(Server.MapPath("~/web/slider_img/"), FileName);
            db.TBLRESIMLER.Remove(res);
            db.SaveChanges();

            if (System.IO.File.Exists(dosyayolu))
            {
                System.IO.File.Delete(dosyayolu);
            }
            return RedirectToAction("ResimGoruntule");
        }


        public ActionResult LinqKartlar()
        {
            return View();
        }
    }
}