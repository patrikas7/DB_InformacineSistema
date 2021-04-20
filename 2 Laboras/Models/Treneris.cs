using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _2_Laboras.Models
{
    public class Treneris
    {
        [Required]
        public string Vardas { get; set; }

        [Required]
        public string Pavarde { get; set; }

        [Required]
        public string Tautybe { get; set; }

        public int id { get; set; }
    }
}