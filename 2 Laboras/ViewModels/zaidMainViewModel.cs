using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _2_Laboras.ViewModels
{
    public class zaidMainViewModel
    {
        public List<ZaidejasViewModel> zaidejai { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? nuo { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? iki { get; set; }
    }
}