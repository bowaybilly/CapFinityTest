using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniCapptivate.Models;
using MiniCapptivate.Services;

namespace MiniCapptivate.Controllers
{
    /// <summary>
    ///  Controller should allow future version example
    ///  [Route("api/v1/{endpoint}")]
    /// </summary>
    /// <comment>
    ///  Api must be architected to allow logging.
    ///  No design based architecture was used in Api. It
    ///  could use Service based or Repository pattern.
    ///  Api will be difficult to test as no abstraction was used
    ///  in data access.
    /// </comment>
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentsController : ControllerBase
    {
        private readonly CapptivateDbContext _context;
        private readonly AssessmentService assessmentService;

        public AssessmentsController(CapptivateDbContext context, AssessmentService assessmentService)
        {
            _context = context;
            this.assessmentService = assessmentService;
        }

        // GET: api/Assessments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Assessment>>> GetAssessment()
        {
            return await _context.Assessments.Include(a => a.Questions).ToListAsync();
        }
        //Implicit route parameters should have the proper [FromRoute] modifier
        // GET: api/Assessments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Assessment>> GetAssessment(int id)
        {
            var assessment = await _context.Assessments
                .Include(u => u.Questions)
                .ThenInclude(u => u.Choices)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (assessment == null)
            {
                return NotFound();
            }

            return assessment;
        }

        // GET: api/Assessments/instances
        [HttpGet("instances")]
        public async Task<ActionResult<IEnumerable<AssessmentResultDto>>> GetAssessmentInstances()
        {
            return await _context.AssessmentInstances
                .Include(ai => ai.Answers)
                .Select(i => new AssessmentResultDto
                {
                    AssessmentInstanceId = i.Id,
                    AssessmentName = i.Assessment.Name,
                    Email = i.Email,
                    CompletedDate = i.CompletedDate,
                    
                })
                .ToListAsync();
        }

        // GET: api/Assessments/instances/5
        [HttpGet("instances/{id}")]
        public async Task<ActionResult<AssessmentInstance>> GetAssessmentInstance(int id)
        {
            var instance = await _context.AssessmentInstances
                .Include(ai => ai.Answers)
                .ThenInclude(a => a.Question)
                .ThenInclude(q => q.Choices)
                .FirstOrDefaultAsync(ai => ai.Id == id);

            if (instance == null) return NotFound();

            return instance;
        }
        // route naming not consistent throughout GET: api/assessment/createInstance 
        // GET: api/createInstance
        [HttpPut("createInstance")]
        public async Task<IActionResult> CreateAssessmentInstance(NewAssessmentInstanceDto dto)
        {
            if (await _context.AssessmentInstances.AnyAsync(a => a.AssessmentId == dto.AssessmentId && a.Email == dto.Email))
            {
                return BadRequest("Email has been already used for this Assessment.");
            }

            var assessment = await _context.Assessments.Include(a => a.Questions).FirstOrDefaultAsync(a => a.Id == dto.AssessmentId);

            if (assessment == null)
            {
                return BadRequest("Assessment doesn't exist");
            }

            var newInstance = new AssessmentInstance
            {
                AssessmentId = assessment.Id,
                Email = dto.Email,
                Answers = assessment.Questions.Select(q => new Answer
                {
                    QuestionId = q.Id,
                })
                .ToList(),
            };

            _context.AssessmentInstances.Add(newInstance);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAssessmentInstance), new { id = newInstance.Id }, newInstance);
        }

        // PUT: api/Assessments/answerQuestion/5
        [HttpPut("answerQuestion/{id}")]
        public async Task<IActionResult> PutAnswer(int id, [FromBody] Answer answer)
        {
            if (id != answer.Id)
            {
                return BadRequest();
            }
            var match = await _context.AssessmentInstances.FirstOrDefaultAsync(x => x.Id == answer.AssessmentInstanceId);
            match.CompletedDate = DateTime.Now;
            _context.Entry(answer).State = EntityState.Modified;
            _context.Entry(match).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Assessments/results
        [HttpGet("results")]
        public async Task<ActionResult<IEnumerable<AssessmentResultDto>>> GetAssessmentResults()
        {
            var instances= await _context.AssessmentInstances
                .Include(i => i.Answers)
                .Select( i => new AssessmentResultDto
                {
                    AssessmentInstanceId = i.Id,
                    AssessmentName = i.Assessment.Name,
                    Email = i.Email,
                    CompletedDate=i.CompletedDate,
                    TotalScore = this.assessmentService.GetScoreAsync(i).Result
                })
                .ToListAsync();
            return instances;
        }
    }
}
