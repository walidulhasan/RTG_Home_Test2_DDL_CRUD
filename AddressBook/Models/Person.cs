using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.Models
{
    public class Person
    {
        [Key]
        public int personId { get; set; }
        [Required(ErrorMessage = "Name is Required"),Display(Name ="Your Name"),StringLength(50)]
        public string personName { get; set; }
        [Required(ErrorMessage = "Phone is Required"), Display(Name = "Number"), StringLength(50)]
        //[Range(1, int.MaxValue, ErrorMessage = "Phone must be greater than 0!")]
        //[RegularExpression(@"/(^(\+88|0088)?(01){1}[3456789]{1}(\d){8})$/",ErrorMessage ="Phone number Format isn't correct")]
        public string personPhone { get; set; }
        public string personPicture { get; set; }

        [DataType(DataType.Date),Display(Name ="Date of Birth")]
        public DateTime dob { get; set; }

        public string village { get; set; }

        [ForeignKey("country"),Display(Name ="Country")]
        public int countryId { get; set; }
        [ForeignKey("division"),Display(Name ="Division")]
        public int divisionId { get; set; }
        [ForeignKey("district"),Display(Name ="District")]
        public int districtId { get; set; }

        //Nav
        public Country country { get; set; }
        public Division division { get; set; }
        public District district { get; set; }
    }
}
