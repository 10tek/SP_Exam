using Exam.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exam
{
    public class ExamContext : DbContext
    {
        public ExamContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Result> Results { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=A-104-15;Database=ExamDb;Trusted_Connection=True;");
        }
    }
}
