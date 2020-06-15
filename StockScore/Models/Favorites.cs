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
        public int Id { get; set; }
        [Display(Name = "Stock Symbol")]
        public string StockSymbol { get; set; }
        [Display(Name = "Time Frame")]
        public string TimeFrame { get; set; }
        //Do they want to buy for a week, month, or year?

        [ForeignKey("User")]
        [Display(Name = "User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
