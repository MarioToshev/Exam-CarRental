using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Data.Entities
{
    [Table("Status")]
    public class RentStatus
    {
        //Not used
        [Key]
        public string Id { get; set; }
        public string StatusName { get; set; }
    }
}
