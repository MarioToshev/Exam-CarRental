using CarRental.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Model.Entities
{
    public class RentFormViewModel
    {
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double TotalPrice { get; set; }
        public string CarId { get; set; }
        public CarViewModel Car { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
    }
}
