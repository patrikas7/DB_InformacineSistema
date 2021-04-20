using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _2_Laboras.ViewModels
{
    public class TrenerisEditViewModel
    {
        [DisplayName("ID")]
        [Required]
        public int id { get; set; }

        [DisplayName("Vardas")]
        [Required]
        public string Vardas { get; set; }

        [DisplayName("Pavarde")]
        [Required]
        public string Pavarde { get; set; }

        [DisplayName("Tautybė")]
        [Required]
        public string Tautybe { get; set; }
    }
}