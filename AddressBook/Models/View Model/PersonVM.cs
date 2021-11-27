using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.Models.View_Model
{
    public class PersonVM
    {
        public int personId { get; set; }

        public string personName { get; set; }

        public string personPhone { get; set; }
        public string personPicture { get; set; }
        public DateTime dob { get; set; }

        public string village { get; set; }
        public int countryId { get; set; }
        public int divisionId { get; set; }
        public int districtId { get; set; }
        public int Age { get; set; }
        public IFormFile PicturPath { get; set; }
    }
}
