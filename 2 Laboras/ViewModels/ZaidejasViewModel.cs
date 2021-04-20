using System;
using System.ComponentModel;

namespace _2_Laboras.ViewModels
{
    public class ZaidejasViewModel
    {
        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("Vardas")]
        public string Vardas { get; set; }

        [DisplayName("Pavardė")]
        public string Pavarde { get; set; }

        [DisplayName("Amžius")]
        public int Amzius { get; set; }

        [DisplayName("Pozicija")]
        public string Pozicija { get; set; }

        [DisplayName("Tautybė")]
        public string Tautybe { get; set; }

        [DisplayName("Komanda")]
        public string Komanda { get; set; }
    }
}