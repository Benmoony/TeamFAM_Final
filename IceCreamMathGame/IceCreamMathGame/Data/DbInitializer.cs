using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IceCreamMathGame.Models;

namespace IceCreamMathGame.Data
{
    public class DbInitializer
    {
        public static void Initialize(IceCreamContext context)
        {
            if (context.Instructors.Any())
            {
                return;
            }

            var instructors = new Instructor[]
            {
                new Instructor{ LastName="Meyers",FirstName="Bob",UserName="BbobM",Password="pass", Email="BobMM@gmail.com"},
                new Instructor{ LastName="Meyers",FirstName="Sally",UserName="SallyM",Password="asdf", Email="SallyM@aol.com"}
            };
            foreach (Instructor i in instructors)
            {
                context.Instructors.Add(i);
            }
            context.SaveChanges();

            var students = new Student[]
            {
                new Student{ LastName="Sue",FirstName="Mary", InstructorID=1},
                new Student{ LastName="Miller",FirstName="Tim", InstructorID=1},
                new Student{ LastName="Shriver",FirstName="Kathy", InstructorID=1},
                new Student{ LastName="Flynn",FirstName="Michael", InstructorID=2},
                new Student{ LastName="Undead",FirstName="Chosen", InstructorID=2},
                new Student{ LastName="Punisher",FirstName="The", InstructorID=2}
            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var scores = new Score[]
            {
                new Score { ScoreNum=9, StudentID=6, Date=DateTime.Parse("2017-05-04") },
                new Score { ScoreNum=5, StudentID=2, Date=DateTime.Parse("2017-05-04") },
                new Score { ScoreNum=8, StudentID=3, Date=DateTime.Parse("2017-05-04") },
                new Score { ScoreNum=4, StudentID=4, Date=DateTime.Parse("2017-05-04") },
                new Score { ScoreNum=7, StudentID=1, Date=DateTime.Parse("2017-05-04") }
            };

            foreach (Score c in scores)
            {
                context.Scores.Add(c);
            }
            context.SaveChanges();
        }
    }
}
