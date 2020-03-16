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
        int Id { get; set; }
        string StockSymbol { get; set; }
        string PurchaseDate { get; set; }

        [ForeignKey("IdentityUser")]
        [Display(Name = "Identity User")]
        string UserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
