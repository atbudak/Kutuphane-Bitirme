using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.EntityFramework;

namespace MvcKutuphane.Controllers
{   [AllowAnonymous]
    public class KayitOlController : Controller
    {
        // GET: KayitOl
        DBLIBRARYEntities db = new DBLIBRARYEntities();

        [HttpGet]
        public ActionResult Kaydol()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Kaydol(TBLUYELER t)
        {
            db.TBLUYELER.Add(t);
            db.SaveChanges();
            return View();
        }
    }
}