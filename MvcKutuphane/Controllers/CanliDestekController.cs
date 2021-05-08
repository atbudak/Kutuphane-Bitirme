using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcKutuphane.Controllers
{
    [Authorize(Roles ="Yetkili")]
    public class CanliDestekController : Controller
    {
        // GET: CanliDestek
        public ActionResult Anasayfa()
        {
            return View();
        }
    }
}