using CarRental.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Models.User
{
    public class UserViewModel
    {
        public string Username { get; set; }
        public  string Role { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string EGN { get; set; }
        public List<RentFormViewModel> rentForms { get; set; }
    }
}
