using System;
using System.ComponentModel;


namespace _2_Laboras.ViewModels
{
    public class KrepsinioKomandaViewModel
    {
        [DisplayName("Komandos pavadinimas")]
        public string Pavadinimas { get; set; }
        [DisplayName("Miestas")]
        public string Miestas { get; set; }
        [DisplayName("Treneris")]
        public string Treneris { get; set; }
        [DisplayName("Biudžetas")]
        public int Biudzetas { get; set; }

    }
}