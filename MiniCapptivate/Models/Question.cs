using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiniCapptivate.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        public string Text { get; set; }

        public QuestionType QuestionType { get; set; }

        public int AssessmentId { get; set; }

        public List<Choice> Choices { get; set; }
        /// <summary>
        /// Min value to set for a slider
        /// </summary>
        public int Min { get;  set; }
        /// <summary>
        /// Maximum value to set for a slider
        /// </summary>
        public int Max { get;  set; }
        /// <summary>
        /// Minimum slider label
        /// </summary>
        public string MinLabel { get;  set; }
        /// <summary>
        /// Max slider label
        /// </summary>
        public string MaxLabel { get;  set; }
        /// <summary>
        /// Default slider position
        /// </summary>
        public int DefaultValue { get; set; } = 0;
    }
}
