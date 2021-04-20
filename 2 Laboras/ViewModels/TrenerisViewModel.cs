using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace _2_Laboras.ViewModels
{
    public class TrenerisViewModel
    {
        [DisplayName("Vardas")]
        public string Vardas { get; set; }

        [DisplayName("Pavarde")]
        public string Pavarde { get; set; }

        [DisplayName("Tautybė")]
        public string Tautybe { get; set; }

    }
}