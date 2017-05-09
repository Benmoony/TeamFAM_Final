using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IceCreamMathGame.Models
{
    public class StudentM
    {
        [Key]
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //[ForeignKey("InstructorID")]
        public int InstructorID { get; set; }

    }
}
