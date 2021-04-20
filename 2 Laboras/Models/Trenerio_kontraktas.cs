using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _2_Laboras.Models
{
    public class Trenerio_kontraktas
    {
        public DateTime Kontrakto_pradzia { get; set; }
        public DateTime Kontrakto_pabaiga { get; set; }
        public int Alga { get; set; }
        public int fk_krepsinio_komanda { get; set; }
        public int fk_treneris { get; set; }
    }
}