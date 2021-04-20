using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _2_Laboras.ViewModels
{
    public class ZaidejasEditViewModel
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

        [DisplayName("Amžius")]
        [Required]
        public int Amzius { get; set; }

        [DisplayName("Pozicija")]
        [Required]
        public string Pozicija { get; set; }

        public string Komanda { get; set; }

        [DisplayName("Tautybė")]
        [Required]
        public string Tautybe { get; set; }

        [DisplayName("Numeris")]
        [Required]
        public int Numeris { get; set; }

        [DisplayName("Taškų vidurkis")]
        [Required]
        public int TaskuVidurkis { get; set; }

        [DisplayName("Alga")]
        [Required]
        public int Alga { get; set; }



        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime KontraktoPradzia { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime KontraktoPabaiga { get; set; }

        public IList<SelectListItem> KomandosList { get; set; }
    }
}