using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _2_Laboras.Models
{
    public class Arena
    {
        [DisplayName("Pavadinimas")]
        [Required]
        public string Pavadinimas { get; set; }

        [DisplayName("Talpa")]
        [Required]
        public int Talpa { get; set; }

        [DisplayName("Miestas")]
        [Required]
        public string Miestas { get; set; }

        [DisplayName("Pastatymo metai")]
        [Required]
        public int Pastatymo_metai { get; set; }

        [DisplayName("Adresas")]
        [Required]
        public string Adresas { get; set; }
        public int id { get; set; }
        public int fk_miestas { get; set; }

    }
}