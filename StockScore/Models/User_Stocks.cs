using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Display(Name = "Stock Symbol")]
        public string StockSymbol { get; set; }
        [Display(Name = "Purchase Date")]
        public string PurchaseDate { get; set; }
        [NotMapped]
        public List<int> Scores { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
} 