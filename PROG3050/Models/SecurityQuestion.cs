using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PROG3050.Models
{
    public class SecurityQuestion
    {
        [Key, Column(Order = 1)]
        public string AccountName { get; set; }
        [Key, Column(Order = 2)]
        [Required]
        public string Question { get; set; }
        [Required]
        public string Answer { get; set; }

    }
}