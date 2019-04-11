using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProiectIP.Models
{
    public class Place
    {
        [Key]
        public String PlaceId { get; set; }

        public virtual ICollection<RatingModel> RatingModels { get; set; }
    }
}