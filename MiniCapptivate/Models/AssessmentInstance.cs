using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiniCapptivate.Models
{
    public class AssessmentInstance
    {
        [Key]
        public int Id { get; set; }

        public int AssessmentId { get; set; }

        public string Email { get; set; }

        public DateTime? CompletedDate { get; set; }

        public Assessment Assessment { get; set; }

        public List<Answer> Answers { get; set; }
        public int TotalScore { get;  set; }
    }
}