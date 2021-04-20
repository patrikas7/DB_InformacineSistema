using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using _2_Laboras.Repos;
using _2_Laboras.Models;


namespace _2_Laboras.ViewModels
{
    public class AsistentasEditViewModel
    {
        [DisplayName("Vardas")]
        [Required]
        public string Vardas { get; set; }

        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("Pavardė")]
        [Required]
        public string Pavarde { get; set; }

        [DisplayName("Tautybė")]
        [Required]
        public string Tautybe { get; set; }

        [DisplayName("Treneris")]
        [Required]
        public int fk_treneris { get; set; }

        public IList<SelectListItem> TreneriaiList { get; set; }

    }
}