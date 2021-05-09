using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRental.Data.Identity;

namespace CarRental.Data.Entities
{
    public class RentForm
    {
        [Key]
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [ForeignKey("CarId")]
        public Car Car { get; set; }
        public string CarId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public string UserId { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; }
    }
}
