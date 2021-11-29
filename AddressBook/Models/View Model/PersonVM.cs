using AddressBook.Custom_Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.Models.View_Model
{
    public class PersonVM
    {
        public int personId { get; set; }
        [Required(ErrorMessage = "Phone is Required", AllowEmptyStrings = false), Display(Name = "Number"), StringLength(50)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        
        public string personName { get; set; }
        [Required(ErrorMessage ="Phone number is Required")]
        [DataType(DataType.PhoneNumber)]
        public string personPhone { get; set; }
        public string personPicture { get; set; }
        [Required(ErrorMessage = "DOB Required", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Display(Name = "Date of Birth")]
        [MinimumAge(18)]
        public DateTime dob { get; set; }

        public string village { get; set; }
        [Required(ErrorMessage = "Country is Required")]
        public int countryId { get; set; }
        [Required(ErrorMessage = "Divison is Required")]
        public int divisionId { get; set; }
        [Required(ErrorMessage = "District is Required")]
        public int districtId { get; set; }
        public int Age { get; set; }
        public IFormFile PicturPath { get; set; }
    }
}
