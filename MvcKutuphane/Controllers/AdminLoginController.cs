using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.EntityFramework;
using System.Web.Security;
namespace MvcKutuphane.Controllers
{   [AllowAnonymous]
    public class AdminLoginController : Controller
    {
        DBLIBRARYEntities db = new DBLIBRARYEntities();
        // GET: AdminLogin
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(TBLADMIN t)
        {
            var kullanici = db.TBLADMIN.FirstOrDefault(x => x.KULLANICIADI == t.KULLANICIADI && x.SIFRE == t.SIFRE);
            if(kullanici != null)
            {
                FormsAuthentication.SetAuthCookie(kullanici.KULLANICIADI, false);
                Session["kullanici"] = (string)kullanici.KULLANICIADI.ToString();
                return RedirectToAction("Index", "Istatistik");
            }
            else
            {
                return View();
            }
            
            
        }
    }
}