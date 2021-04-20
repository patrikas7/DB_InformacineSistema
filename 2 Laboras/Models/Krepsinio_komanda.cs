using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace _2_Laboras.Models
{
    public class Krepsinio_komanda
    {
        [DisplayName("ID")]
        [Required]
        public int id { get; set; }

        [DisplayName("Pavadinimas")]
        [Required]
        public string Pavadinimas { get; set; }

        [DisplayName("Miestas")]
        [Required]
        public string Miestas { get; set; }

        [DisplayName("Treneris")]
        [Required]
        public string Treneris { get; set; }

        [DisplayName("Biudžetas")]
        [Required]
        public int Biudzetas { get; set; }

        [DisplayName("Arena")]
        [Required]
        public string Arena { get; set; }

        [DisplayName("Lygų licenzijos")]
        [Required]
        public string Lygu_licenzija { get; set; }

        [DisplayName("Laimėjimai")]
        [Required]
        public string Laimejimai { get; set; }
       
        public virtual IEnumerable<Darbuotojas> Darbuotojai { get; set; }
    }
}