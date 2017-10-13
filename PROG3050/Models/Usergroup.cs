using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PROG3050.Models
{
    public class Usergroup
    {
        [Key]
        public string Title { get; set; }
        public int GroupPermissions { get; set; }
    }
}