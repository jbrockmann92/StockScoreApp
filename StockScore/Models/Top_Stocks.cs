using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockScore.Models
{
    public class Top_Stocks
    {
        [Key]
        public int Id { get; set; }
        public string numberOne { get; set; }
        public string numberTwo { get; set; }
        public string numberThree { get; set; }
        public string numberFour { get; set; }
    }
}
