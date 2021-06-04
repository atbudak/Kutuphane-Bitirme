using MvcKutuphane.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane.Controllers
{
    public class HakkimizdaController : Controller
    {
        // GET: Hakkimizda
        DBLIBRARYEntities db = new DBLIBRARYEntities();
        public ActionResult Index()
        {
            var about = db.TBLHAKKIMIZDA.ToList();
            return View(about);
        }

        public ActionResult HakkimizdaDetay(int id) 
        {
            var detay = db.TBLHAKKIMIZDA.Find(id);
            return View("HakkimizdaDetay", detay);
        }

        public ActionResult HakkimizdaGuncelle(TBLHAKKIMIZDA h)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("HakkimizdaDetay/" + h.ID);
            }
            var hakkimizda = db.TBLHAKKIMIZDA.Find(h.ID);
            hakkimizda.ACIKLAMA = h.ACIKLAMA;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}