using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProiectIP.Models
{
    public class RatingModel
    {
        [Key]
        [Column(Order = 0)]
        public String UserId { get; set; }
        [Key]
        [Column(Order = 1)]
        public String PlaceId { get; set; }

        public float Rating { get; set; }
        public int UnixTimestamp { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Place Place { get; set; }
    }
}