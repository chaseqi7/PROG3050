using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PROG3050.Models
{
    public class Game
    {
        public int GameID { get; set; }
        public string Title { get; set; }
        public string Platform { get; set; }
        public string Description { get; set; }
        public string Developer { get; set; }
        public string Publisher { get; set; }
        public string Genre { get; set; }
        public string EsrbRating { get; set; }

        [Range(typeof(decimal), "0", "400.00")  ]
        public decimal Price { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }

    }
}