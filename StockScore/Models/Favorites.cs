using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockScore.Models
{
    public class Favorites
    {
        [Key]
        int Id { get; set; }
        string StockSymbol { get; set; }
        string TimeFrame { get; set; }
        //Do they want to buy for a week, month, or year?

        [ForeignKey("IdentityUser")]
        [Display(Name = "Identity User")]
        string UserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
