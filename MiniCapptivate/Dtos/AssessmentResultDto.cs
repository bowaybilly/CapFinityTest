using System;

namespace MiniCapptivate.Models
{
    public class AssessmentResultDto
    {
        public int AssessmentInstanceId { get; set; }
        public string Email { get; set; }
        public string AssessmentName { get; set; }
        public DateTime? CompletedDate { get; set; }
        public int TotalScore { get;  set; }
    }
}