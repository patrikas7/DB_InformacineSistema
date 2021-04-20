using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _2_Laboras.Models
{
    public class Darbuotojas
    {
        [DisplayName("Vardas")]
        [Required]
        public string Vardas { get; set; }

        [DisplayName("Pavarde")]
        [Required]
        public string Pavarde { get; set; }


        [DisplayName("Einamos pareigos")]
        [Required]
        public string EinamosPareigos { get; set; }
    
        public int id { get; set; }
        public int fk_krepsinioKomanda { get; set; }
    }
}