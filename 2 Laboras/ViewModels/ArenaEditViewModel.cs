using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace _2_Laboras.ViewModels
{
    public class ArenaEditViewModel
    {
        [DisplayName("ID")]
        [Required]
        public int id { get; set; }

        [DisplayName("Pavadinimas")]
        [Required]
        public string Pavadinimas { get; set; }

        [DisplayName("Talpa")]
        [Required]
        public int Talpa { get; set; }
        public string Miestas { get; set; }

        [DisplayName("Adresas")]
        [Required]
        public string Adresas { get; set; }

        [DisplayName("Pastatymo metai")]
        [Required]
        public int Pastatymo_metai { get; set; }

        [DisplayName("Miestas")]
        [Required]
        public int fk_miestas { get; set; }

        public IList<SelectListItem> MiestaiList { get; set; }

    }
}