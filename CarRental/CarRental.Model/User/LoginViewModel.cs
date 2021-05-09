using System;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Models.User
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
      
    }
}
