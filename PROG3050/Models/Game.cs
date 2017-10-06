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
        public string FrenchName { get; set; }
        public string GameStatusCode { get; set; }
        public int GameCategoryId { get; set; }
        public Boolean FrenchVersion { get; set; }
        public string EsrbRatingCode { get; set; }
        public string UserName { get; set; }        
        public string EnglishDescription { get; set; }
        public string EnglishDetail { get; set; }
        
    }
}