using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.Models
{
    public class Country
    {
        [Key]
        public int countryId { get; set; }
        [Required(ErrorMessage = "Country is required")]
        [StringLength(50),Display(Name ="Country")]
        public string countryName { get; set; }
        public ICollection<Division> division { get; set; }

    }
}
