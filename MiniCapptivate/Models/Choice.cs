using System.ComponentModel.DataAnnotations;

namespace MiniCapptivate.Models
{
    public class Choice
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
