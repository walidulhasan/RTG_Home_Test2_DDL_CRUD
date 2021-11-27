using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.Models
{
    public class District
    {
        [Key]
        public int districtId { get; set; }
        [Required(ErrorMessage = ("District Name is required"))]
        [StringLength(50), Display(Name = "District")]
        public string districtName { get; set; }

        public int DivisionId { get; set; }
        public Division division { get; set; }

    }
}
