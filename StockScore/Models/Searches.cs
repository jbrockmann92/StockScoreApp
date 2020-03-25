using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StockScore.Models
{
    public class Searches
    {
        [Key]
        public int Id { get; set; }
        public string Symbol { get; set; }
        public float Score { get; set; }
        public string TimeFrame { get; set; }
        public bool IsForPastScores { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
