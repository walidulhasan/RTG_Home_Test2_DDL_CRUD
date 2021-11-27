using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.Models
{
    public class Division
    {
        [Key]
        public int divisionId { get; set; }
        [Required(ErrorMessage =("Division Name is required"))]
        [StringLength(50),Display(Name ="Division")]
        public string divisionName { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<District> districts { get; set; }
    }
}
