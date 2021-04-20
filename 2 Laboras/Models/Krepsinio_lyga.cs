using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace _2_Laboras.Models
{
    public class Krepsinio_lyga
    {
        [DisplayName("Lygos pavadinimas")]
        [Required]
        public string Lygos_pavadinimas { get; set; }

        [DisplayName("Prizinis fondas")]
        [Required]
        public int Prizinis_fondas { get; set; }

        [DisplayName("Komandų skaičius")]
        [Required]
        public int Komandu_skaicius { get; set; }

        [DisplayName("Turnyro trukmė")]
        [Required]
        public string Turnyro_trukme { get; set; }

        [DisplayName("Formatas")]
        [Required]
        public string Formatas { get; set; }

        public string Remejas { get; set; }

        [DisplayName("ID")]
        [Required]
        public int id { get; set; }
    }
}