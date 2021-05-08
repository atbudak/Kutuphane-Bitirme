using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcKutuphane.Models.EntityFramework;

namespace MvcKutuphane.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        DBLIBRARYEntities db = new DBLIBRARYEntities();
        // GET: Login
        public ActionResult GirisYap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GirisYap(TBLUYELER t)
        {
            var bilgiler = db.TBLUYELER.FirstOrDefault(x => x.MAIL == t.MAIL && x.SIFRE == t.SIFRE);
            if(bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.MAIL, false);
                Session["Mail"] = bilgiler.MAIL.ToString();
                Session["Ad"] = bilgiler.AD.ToString();
                Session["Soyad"] = bilgiler.SOYAD.ToString();

                if (bilgiler.YETKILER.Contains("Yetkili"))
                {
                    return RedirectToAction("Anasayfa", "CanliDestek");
                }
                else
                {
                    return RedirectToAction("Profil", "Panel");
                }

            }
            else
            {
                return View();
            }
        }
    }
}