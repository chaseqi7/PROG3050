using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PROG3050.Models
{
    public class SecurityQuestion
    {
        [Key]
        public string AccountName { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

    }
}