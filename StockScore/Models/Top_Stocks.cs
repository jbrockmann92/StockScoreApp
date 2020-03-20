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
        public DateTime date { get; set; }
        public string[] possibleTopStocks = new string[] { "MMM", "AXP", "AAPL", "BA", "CAT", "CVX", "CSCO", "KO", "DIS", "DOW", "XOM", "GS", "HD", "IBM", "INTC", "JNJ", "JPM", "MCD", "MRK", "MSFT", "NKE", "PFE", "PG", "TRV", "UTX", "UNH", "VZ", "V", "WMT","WBA" };
    }
}
