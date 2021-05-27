using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MiniCapptivate.Models
{

    public class CapptivateDbContext : DbContext
    {
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AssessmentInstance> AssessmentInstances { get; set; }
        public DbSet<Choice> Choices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /// Proper way of using fluent api to seed data
            // modelBuilder.Entity<ExampleClass>(configure => { new List<ExampleClass>(){new (){}....} });
            base.OnModelCreating(modelBuilder);
        }
        public CapptivateDbContext(DbContextOptions<CapptivateDbContext> options)
            : base(options)
        {
            SeedData();
        }
        /// <summary>
        /// Seed data can be moved to its own class
        /// Fluent API can be better used instead of seeding via a method
        /// </summary>
        private void SeedData()
        {
            // return if data has been seeded already
            if (Assessments.Any()) return;

            var generalKnowledgeTest = new Assessment
            {
                Id = 1,
                Name = "General Knowledge Test",
                Questions = new List<Question> {
                    new Question { Id=1,Text = "What colour is a (ripe) banana?" },
                    new Question { Id=2, Text = "How many continents are there?", QuestionType = QuestionType.Number },
                    new Question {  Id=3,Text = "How many players are there in a rugby league team?", QuestionType = QuestionType.Number },
                    new Question
                    {
                         Id=4,
                        Text = "Who didn't appear in 'Inception'",
                        QuestionType = QuestionType.Choice,
                        Choices = new List<Choice>
                        {
                            new Choice { Text = "Leonardo DiCaprio" },
                            new Choice { Text = "Tom Hardy" },
                            new Choice { Text = "Tom Cruise" },
                        },
                    },
                    new Question {  Id=5, Text = "What is David Bowie’s real name?", QuestionType= QuestionType.Text },
                }
            };
            var mathQuiz = new Assessment
            {
                Id = 2,
                Name = "Math Quiz",
                Questions = new List<Question> {
                    new Question {  Id=6, Text = "2 + 2 = ?", QuestionType = QuestionType.Number},
                    new Question {   Id=7,Text = "2 + 2 * 2 = ?", QuestionType = QuestionType.Number},
                    new Question
                    {
                        Id=8,
                        Text = "Who came up with 'Pythagorean theorem'?",
                        QuestionType = QuestionType.Choice,
                        Choices = new List<Choice>
                        {
                            new Choice { Text = "Aristotle" },
                            new Choice { Text = "Kurt Gödel" },
                            new Choice { Text = "Pythagoras" },
                            new Choice { Text = "Bertrand Russell" },
                        },
                    },
                    new Question {  Id=9, Text = "What mathematical term is given to 3.141592?", QuestionType = QuestionType.Text},
                    new Question {  Id=10,
                                    Text = "How many sides has a hexagon?",
                                    QuestionType = QuestionType.Slider,
                                    Min=1,
                                    Max=8,
                                    MinLabel="1",
                                    MaxLabel="8"
                                 },
                    new Question {  Id=11,
                                    Text = "What do you think of this questionnaire?",
                                    QuestionType = QuestionType.Slider,
                                    Min=1,
                                    Max=10,
                                    MinLabel="Awful",
                                    MaxLabel="Awesome",
                                    DefaultValue=0
                                 },
                }
            };

            var answers = new List<QuestionAnswer>() {
              new QuestionAnswer(){ Id=1,AssessmentID=1, QuestionID=1, Answer="Yellow"} ,
              new QuestionAnswer(){ Id=2,AssessmentID=1, QuestionID=2, Answer="7"} ,
              new QuestionAnswer(){ Id=3,AssessmentID=1, QuestionID=3, Answer="13"} ,
              new QuestionAnswer(){ Id=4,AssessmentID=1, QuestionID=4, Answer="Tom Cruise"} ,
              new QuestionAnswer(){ Id=5,AssessmentID=1, QuestionID=5, Answer="David Robert Jones"} ,

            new QuestionAnswer(){ Id=6, AssessmentID=2, QuestionID=6, Answer="4"} ,
            new QuestionAnswer(){ Id=7, AssessmentID=2, QuestionID=7, Answer="6"} ,
            new QuestionAnswer(){ Id=8, AssessmentID=2, QuestionID=8, Answer="Pythagoras"} , 
            new QuestionAnswer(){ Id=9, AssessmentID=2, QuestionID=9, Answer="PI"} ,
            new QuestionAnswer(){ Id=10,AssessmentID=2, QuestionID=10, Answer="6"} ,
            new QuestionAnswer(){ Id=11,AssessmentID=2, QuestionID=11, Answer="",IsScorable=false} ,
            };
            QuestionAnswers.AddRange(answers);
            Assessments.AddRange(generalKnowledgeTest, mathQuiz);

            SaveChanges();
        }
    }
}
