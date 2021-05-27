using Microsoft.EntityFrameworkCore;
using MiniCapptivate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniCapptivate.Services
{
    /// <summary>
    /// Services to use for all assestment related actions
    /// </summary>
    public class AssessmentService
    {
        private readonly CapptivateDbContext _context;

        public AssessmentService(CapptivateDbContext context)
        {
            this._context = context;
        }
        /// <summary>
        /// Computes assessment score
        /// </summary>
        /// <param name="assessmentInstance"></param>
        /// <returns></returns>
        public async Task<int> GetScoreAsync(AssessmentInstance  assessmentInstance)
        {
            var instance = await _context.AssessmentInstances
                .Include(ai => ai.Answers)
                .ThenInclude(a => a.Question)
                .ThenInclude(q => q.Choices)
                .FirstOrDefaultAsync(ai => ai.Id == assessmentInstance.Id);
            var match = from i in instance.Answers
                        join a in _context.QuestionAnswers.AsEnumerable() on i.QuestionId equals a.QuestionID
                        where (i.Question.QuestionType!= QuestionType.Choice? i.Text?.ToString() == a.Answer: i.Choice.Text == a.Answer)  && a.IsScorable==true
                        select i;
            return match.Count();
        }
    }
}
