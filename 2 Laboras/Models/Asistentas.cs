using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _2_Laboras.Models
{
    public class Asistentas
    {
        public string Vardas { get; set; }
        public string Pavarde { get; set; }
        public string Tautybe { get; set; }
        public int id { get; set; }
        public int fk_treneris { get; set; }
    }
}