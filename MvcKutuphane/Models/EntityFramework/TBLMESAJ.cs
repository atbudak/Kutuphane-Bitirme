//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcKutuphane.Models.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public partial class TBLMESAJ
    {
        public int ID { get; set; }
        public string GONDEREN { get; set; }
        public string ALICI { get; set; }
        public string KONU { get; set; }
        [AllowHtml]
        public string ICERIK { get; set; }
        public Nullable<System.DateTime> TARIH { get; set; }
        public Nullable<bool> DURUM { get; set; }
    }
}
