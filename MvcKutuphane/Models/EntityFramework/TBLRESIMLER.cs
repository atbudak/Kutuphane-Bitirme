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
    using System.ComponentModel.DataAnnotations;

    public partial class TBLRESIMLER
    {
        public int ID { get; set; }

        [Required(ErrorMessage = " Bu alan zorunludur.")]
        public string RESIMYOLU { get; set; }
        public string RESIMDETAY { get; set; }
        public Nullable<bool> DURUM { get; set; }
    }
}
