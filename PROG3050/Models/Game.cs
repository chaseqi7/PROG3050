using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PROG3050.Models
{
    public class Game
    {
        [Key]
        public Guid Guid { get; set; } 
        public string EnglishName { get; set; }
        public string EnglishDescription { get; set; }
        public string EnglishDetail { get; set; }
        
    }
}