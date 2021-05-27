using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniCapptivate.Models
{
    /// <summary>
    /// Assessment question answers
    /// </summary>
    public class QuestionAnswer
    {
        public int Id { get; set; }
        public int QuestionID { get; set; }
        public string Answer { get; set; }
        /// <summary>
        /// All question answers by default are scorable unless overriden
        /// </summary>
        public bool IsScorable { get; set; } = true;
        public int AssessmentID { get; internal set; }
    }
}
