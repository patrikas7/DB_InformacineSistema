using System;
using System.ComponentModel;

namespace _2_Laboras.ViewModels
{
    public class AsistenasViewModel
    {
        [DisplayName("Vardas")]
        public string Vardas { get; set; }

        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("Pavardė")]
        public string Pavarde { get; set; }

        [DisplayName("Tautybė")]
        public string Tautybe { get; set; }

        [DisplayName("Treneris")]
        public string Treneris { get; set; }



    }

}