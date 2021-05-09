using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Data.Entities
{
    public class Car
    {
        [Key]
        public string Id { get; set; }
        public string Model { get; set; }
        public  DateTime YearOfCreation { get; set; }
        public int PassagerPlaces { get; set; }
        public string Description { get; set; }
        public double PricePerDay { get; set; }
        public string PhotoUrl { get; set; }
        public bool IsRented { get; set; }

    }
}
