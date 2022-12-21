using Microsoft.AspNetCore.Mvc;
using Registry.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Registry.Infrastructure.Data;
using Registry.Infrastructure.Repository;

namespace Registry.API.Controllers
{

    [Route("api/rewards/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly Context _context;
        private readonly LessonRepository _lessonRepository;
        private DisciplineRepository _disciplineRepository;
        public LessonController(Context context)
        {
            _context = context;
            _lessonRepository = new LessonRepository(_context);
            _disciplineRepository = new DisciplineRepository(_context);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lesson>>> GetLesson()
        {
            return await _lessonRepository.GetAllAsync();
        }

  
        [HttpGet("{id}")]
        public async Task<ActionResult<Lesson>> GetAchievement(int id)
        {

            var person = await _lessonRepository.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return person;
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAchievement(int id, Lesson achievement)
        {
            Console.WriteLine("JOJO_REFERENCE_TWO");
            if (id != achievement.Id)
            {
                return BadRequest();
            }

            await _lessonRepository.UpdateAsync(achievement);

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {

            var person = await _lessonRepository.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }


            await _lessonRepository.DeleteAsync(id);

            return NoContent();
        }

        private bool AchievementExists(int id)
        {
            return _context.Lessons.Any(e => e.Id == id);
        }
    }
}
