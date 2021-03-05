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
    public class IslemController : Controller
    {
        DBLIBRARYEntities db = new DBLIBRARYEntities();
        // GET: Islem
        public ActionResult Index(string search, int page = 1)
        {
            var ktp = from x in db.TBLHAREKET select x;
            if (!string.IsNullOrEmpty(search))
            {
                ktp = ktp.Where(x => x.TBLUYELER.AD.ToUpper().Contains(search.ToUpper()) || x.TBLUYELER.SOYAD.ToUpper().Contains(search.ToUpper()) || x.TBLKITAP.AD.ToUpper().Contains(search.ToUpper()));
            }
            return View(ktp.Where(x => x.ISLEMDURUM == true).ToList().ToPagedList(page, 10));
        }
    }
}