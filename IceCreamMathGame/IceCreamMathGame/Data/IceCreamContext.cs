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

        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Score> Scores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Instructor>().ToTable("Instructor");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Score>().ToTable("Score");
        }
    }
}
