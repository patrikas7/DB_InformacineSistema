using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _2_Laboras.ViewModels
{
    public class PrezidentasViewModel
    {
        [DisplayName("ID")]
        [Required]
        public int id { get; set; }

        [DisplayName("Vardas")]
        [Required]
        public string Vardas { get; set; }

        [DisplayName("Pavardė")]
        [Required]
        public string Pavarde { get; set; }
    }
}