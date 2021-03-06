﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public List<int> PastMonthScores { get; set; }
        public List<int> TopStocksIds { get; set; }

        [NotMapped]
        public List<string> TimeFrame = new List<string>() {"Day", "Week", "Month"};
    }
}
