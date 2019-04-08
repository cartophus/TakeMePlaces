using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProiectIP.Models
{
    public class RatingModel
    {
        public int UserId { get; set; }
        public int PlaceId { get; set; }
        public float Rating { get; set; }
        public int UnixTimestamp { get; set; }
    }
}