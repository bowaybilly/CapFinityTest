
namespace MiniCapptivate.Models
{
    public class Answer
    {
        public int Id { get; set; }

        public int AssessmentInstanceId { get; set; }

        public int QuestionId { get; set; }

        public string Text { get; set; }

        public int ChoiceId { get; set; }

        public Question Question { get; set; }

        public Choice Choice { get; set; }
    }
}
