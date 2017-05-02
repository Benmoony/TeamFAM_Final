using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IceCreamMathGame.Models
{
    public class Score
    {
        public int ScoreID { get; set; }
        public int StudentID { get; set; }
        public int ScoreNum { get; set; }
        public DateTime Date { get; set; }
    }
}
