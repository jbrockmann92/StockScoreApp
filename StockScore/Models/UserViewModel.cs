﻿using System;
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

    }
}