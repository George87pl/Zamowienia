using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Zamowienia.Models
{
    public class Zamowienie
    {
        [Required]
        public int ZamowienieID { get; set; }
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        public string Nazwa { get; set; }
        [Required]
        public DateTime DataZlozenia { get; set; }
        public DateTime? DataZakonczenia { get; set; }
    }  
}
