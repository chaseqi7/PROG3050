using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PROG3050.Models
{
    public class Account
    {
        [Key]
        public int UserID { get; set; }
        public string AccountName { get; set; }
        public string UserType { get; set; }
        public string UPassword { get; set; }
        public DateTime Registered { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string StateProvince { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}