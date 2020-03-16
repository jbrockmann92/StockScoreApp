﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockScore.Models
{
    public class Searches
    {
        [Key]
        public int Id { get; set; }
        public string Ticker { get; set; }
        public float Score { get; set; }
        public string DateSearched { get; set; }

    }
}
