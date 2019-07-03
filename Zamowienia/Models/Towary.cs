using System.ComponentModel.DataAnnotations;

namespace Zamowienia.Models
{
    public class Towary
    {
        [Required]
        public int TowaryID { get; set; }
        [Required]
        public int Zamowienie { get; set; }
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        public string Nazwa { get; set; }
        [Required(ErrorMessage = "Ilość jest wymagana")]
        public int Ilosc { get; set; }
    }
}
