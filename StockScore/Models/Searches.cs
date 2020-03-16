using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StockScore.Models
{
    public class Searches
    {
        [Key]
        int Id { get; set; }
        string Ticker { get; set; }
        float Score { get; set; }
        string DateSearched { get; set; }

    }
}
