using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcKutuphane.Models.EntityFramework;

namespace MvcKutuphane.Models.Classes
{
    public class Class1
    {
        public IEnumerable<TBLKITAP>  Kitap1 { get; set; }
        public IEnumerable<TBLHAKKIMIZDA> hakkimizda1 { get; set; }
        public IEnumerable<TBLRESIMLER> slider1 { get; set; }
    }
}