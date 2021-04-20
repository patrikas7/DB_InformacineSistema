using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace _2_Laboras.ViewModels
{
    public class ArenaViewModel
    {
        [DisplayName("Pavadinimas")]
        public string Pavadinimas { get; set; }

        [DisplayName("Talpa")]
        public int Talpa { get; set; }

        [DisplayName("Miestas")]
        public string Miestas { get; set; }

        [DisplayName("Adresas")]
        public string Adresas { get; set; }

        [DisplayName("Pastatymo metai")]
        public int Pastatymo_metai { get; set; }
        
        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("Miestas")]
        public string fk_Miestas { get; set; }
    }
}