using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IceCreamMathGame.Models
{
    public class Score
    {
        [Key]
        public int ScoreID { get; set; }

        [ForeignKey("StudentID")]
        public int StudentID { get; set; }
        public int ScoreNum { get; set; }
        public DateTime Date { get; set; }
    }
}
