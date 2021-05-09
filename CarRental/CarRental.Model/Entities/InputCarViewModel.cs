using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Model.Entities
{
    public class InputCarViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public DateTime YearOfCreation { get; set; }
        [Required]
        public int PassagerPlaces { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double PricePerDay { get; set; }
        [Required]
        public IFormFile PhotoUrl { get; set; }
        
    }
}
