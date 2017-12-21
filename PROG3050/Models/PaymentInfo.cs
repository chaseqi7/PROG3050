using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PROG3050.Models
{
    public class PaymentInfo
    {
        public int PaymentInfoId { get; set; }
        [StringLength(200)]
        public string AccountName { get; set; }
        [StringLength(20)]
        public string CardNumber { get; set; }
        [StringLength(50)]
        public string CardType { get; set; }
        [StringLength(5)]
        public string ExpirationDate { get; set; }
        public int CVC { get; set; }

    }
}