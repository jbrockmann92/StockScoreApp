using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockScore.Models
{
    public class User_Stocks
    {
        [Key]
        public int Id { get; set; }
        public string StockSymbol { get; set; }
        public string PurchaseDate { get; set; }

        [ForeignKey("IdentityUser")]
        [Display(Name = "Identity User")]
        public string UserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
