using System;
using System.ComponentModel;
namespace _2_Laboras.ViewModels
{
    public class ImoneViewModel
    {
        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("Pavadinimas")]
        public string Pavadinimas { get; set; }

        [DisplayName("Gaminama produkcija")]
        public string GaminamaProdukcija { get; set; }
    }
}