using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PROG3050.Models
{
    public class Event
    {
        [Key]
        public int EventID { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Country { get; set; }
        public string StateProvince { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

    }
}