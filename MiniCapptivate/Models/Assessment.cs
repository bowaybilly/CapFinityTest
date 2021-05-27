using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiniCapptivate.Models
{
    public class Assessment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Question> Questions { get; set; }
    }
}
