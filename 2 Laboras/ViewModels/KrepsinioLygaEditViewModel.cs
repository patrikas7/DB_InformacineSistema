using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using _2_Laboras.Models;

namespace _2_Laboras.ViewModels
{
    public class KrepsinioLygaEditViewModel
    {
        [DisplayName("Lygos pavadinimas")]
        [Required]
        public string LygosPavadinimas { get; set; }

        [DisplayName("Prizinis fondas")]
        [Required]
        public int PrizinisFondas { get; set; }

        [DisplayName("Komandų skaičius")]
        [Required]
        public int KomanduSkaicius { get; set; }

        [DisplayName("Turnyro trukmė")]
        [Required]
        public string TurnyroTrukme { get; set; }

        public string Remejas { get; set; }

        [DisplayName("Formatas")]
        [Required]
        public string Formatas { get; set; }

        public int id { get; set; }

        [DisplayName("Lygos remėjas")]
        [Required]
        public int fk_remejas { get; set; }

        public List<PrezidentoKadencija> PrezidentoKadencijos { get; set; }

        public IList<SelectListItem> RemejaiList { get; set; }
        public IList<SelectListItem> PrezidentaiList { get; set; }

    }
}