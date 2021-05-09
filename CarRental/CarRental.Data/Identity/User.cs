using CarRental.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarRental.Data.Identity
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string EGN { get; set; }
        public string Role { get; set; }
        public string RentFormId { get; set; }
        [ForeignKey("RentFormId")]
        public List<RentForm> rentForms { get; set; }
    }
}
