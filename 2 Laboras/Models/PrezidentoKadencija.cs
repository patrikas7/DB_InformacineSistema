using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace _2_Laboras.Models
{
    public class PrezidentoKadencija
    {
        public int fk_prezidentas { get; set; }
        public int id { get; set; }
        public int fk_komanda { get; set; }

        [DisplayName("Vardas")]
        public string vardas { get; set; }

        [DisplayName("Pavardė")]
        public string pavarde { get; set; }
        [DisplayName("Kadencijos pradžia")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime KadencijosPradzia { get; set; }

        [DisplayName("Kadencijos pabaiga")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime KadencijosPabaiga { get; set; }
    }
}