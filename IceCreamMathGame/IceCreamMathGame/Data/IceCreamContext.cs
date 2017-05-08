using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IceCreamMathGame.Models;

namespace IceCreamMathGame.Data
{
    public class IceCreamContext : DbContext
    {
        public IceCreamContext(DbContextOptions<IceCreamContext> options) : base(options)
        {

        }

        public DbSet<InstructorM> Instructors { get; set; }
        public DbSet<StudentM> Students { get; set; }
        public DbSet<Score> Scores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InstructorM>().ToTable("Instructor");
            modelBuilder.Entity<StudentM>().ToTable("Student");
            modelBuilder.Entity<Score>().ToTable("Score");
        }
    }
}
