using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.EntityFramework;

namespace MvcKutuphane.Controllers
{
    public class IstatistikController : Controller
    {
        DBLIBRARYEntities db = new DBLIBRARYEntities();
        // GET: Istatistik
        public ActionResult Index()
        {
            return View();
        }
    }
}