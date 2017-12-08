namespace PROG3050.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Friend")]
    public partial class Friend
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(200)]
        public string AccountName { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(200)]
        public string FriendedAccountName { get; set; }
    }
}
