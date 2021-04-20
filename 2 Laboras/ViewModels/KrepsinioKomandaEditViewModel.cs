using System;
using System.Collections.Generic;
using _2_Laboras.Models;


namespace _2_Laboras.ViewModels
{
    public class KrepsinioKomandaEditViewModel
    {
        public Krepsinio_komanda KrepsinioKomanda { get; set; }

        public List<DarbuotojasViewModel> Darbuotojai { get; set; }
    }
}