using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockScore.Models
{
    public class UserViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Symbol { get; set; }
        public User User { get; set; }
        public List<User_Stocks> Stocks { get; set; }
        public Searches Search { get; set; }
        public List<Searches> Searches { get; set; }
        public Top_Stocks top_Stocks { get; set; }
        public string stockDataJson { get; set; }
        //I don't think I'll need this one
        public List<int> PastMonthScores { get; set; }
        //Could probably be an array. No real reason to though. More flexible this way
    }
}
