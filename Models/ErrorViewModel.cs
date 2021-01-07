using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace dotnetExamTrainer.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
    
    
    public class ExamTrainerContext : DbContext
    {
        public DbSet<Question> Questions { get; set;}
        public DbSet<TakenExam> TakenExams { get; set;}


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            String conString = ("User ID=postgres;" +
                                "Password=none;" +
                                "Host=localhost;" +
                                "port=5432;" +
                                "Database=dotnetExamTrainer;" +
                                "Pooling=true; ");
            optionsBuilder.UseNpgsql(conString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TakenExam>()
                .HasNoKey();
        }
    }
    
    
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string QuestionText { get; set; }

        public string SubText { get; set;}
        public string[] AnswerText { get; set; }
        public int RightAnswer { get; set; }


        public override string ToString()
        {
            string outp = "\n";
            outp += "Id: " + Id + "\n";
            outp += "QuestionText: " + QuestionText + "\n";
            if (!SubText.Equals(null))
            {
                outp += SubText + "\n";
            }

            for (int i = 0; i < AnswerText.Length; i++)
            {
                outp += "- " + AnswerText[i];
                if (i == RightAnswer)
                {
                    outp += "  <- Right";
                }
                outp += "\n";
            }
            return outp;
        }
    }

    
    public class TakenExam
    {
        public string Name { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Taken { get; set; }
        public int AmountAnsweredRight { get; set; }
        public int AmountAnsweredWrong { get; set; }
        public int[] QuestionsAnsweredRight { get; set; }
        public int[] QuestionsAnsweredWrong { get; set; }
    }
}
