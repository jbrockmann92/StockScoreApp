﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockScore.Models
{
    public static class Words
    {
        public static string[] negativeWords = new string[] { "plunged", "dropped", "drop", "bear", "negative", "pessimistic", "plummeted", "nervous", "correction", "plunges", "fall", "falling", "bearish", "warning", "apparently", "down", "plummets", "fear", "concern", "plummet" };

        public static string[] positiveWords = new string[] { "boost", "lowered", "cut", "bull", "positive", "optimistic", "slashed", "bullish", "prosperous", "profit", "ready", "forward", "progress", "profitable", "rebound", "dividend", "dividends", "high", "optimism", "wealth", "confident", "solid", "promising", "good", "exciting", "forward", "excited", "promise" };
    }
}
