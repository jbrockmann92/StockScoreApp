using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockScore.Models
{
    public class Top_Stocks
    {
        [Key]
        public int Id { get; set; }
        public string NumberOne { get; set; }
        public string NumberTwo { get; set; }
        public string NumberThree { get; set; }
        public string NumberFour { get; set; }
        public DateTime date { get; set; }

        public string[] possibleTopStocks = new string[] { "MMM", "AXP", "AAPL", "BA", "CAT", "CVX" };

        //This is the actual one, but making a shorter list for testing
        //public string[] possibleTopStocks = new string[] { "MMM", "AXP", "AAPL", "BA", "CAT", "CVX", "CSCO", "KO", "DIS", "DOW", "XOM", "GS", "HD", "IBM", "INTC", "JNJ", "JPM", "MCD", "MRK", "MSFT", "NKE", "PFE", "PG", "TRV", "UTX", "UNH", "VZ", "V", "WMT","WBA" };

        //[ForeignKey("User")]
        //public string UserId { get; set; }
        //public User user { get; set; }
    }
}
