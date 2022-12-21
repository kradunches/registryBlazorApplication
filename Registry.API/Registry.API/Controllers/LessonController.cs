using Microsoft.AspNetCore.Mvc;
using Registry.Domain.Model;
using Registry.Infrastructure.Data;

namespace Registry.API.Controllers
{
  public class LessonController
  {
    private readonly Context _context;
    private readonly DisciplineRepository _disciplineRepository;
    private LessonRepository _lessonRepository;
    public LessonController(Context context)
    {
      _context = context;
      _disciplineRepository = new DisciplineRepository(_context);
      _lessonRepository = new LessonRepository(_context);
    }
    // GET: api/Lesson/5
    [HttpGet("da/{id}")]
    public async Task<ActionResult<Lesson>> GetDayByDate(string disciplineName)
    {
      return await _lessonRepository.GetByDisciplineNameAsync(disciplineName);
    }

    // GET: api/Lesson/5
    [HttpGet("vis/{id}")]
    public async Task<ActionResult<Lesson>> GetLessonById(int id)
    {

      var lesson = await _lessonRepository.GetByIdAsync(id);
      if (lesson == null)
      {
        return NotFound();
      }
      return lesson;
    }

    // GET: api/Lesson/5
    [HttpGet("lesson/{id}")]
    public async Task<ActionResult<Lesson>> GetLessonByDate(string date)
    {
      var lesson = await _lessonRepository.GetByDateAsync(date);
      if (lesson == null)
      {
        return NotFound();
      }
      return lesson;
    }

    // PUT: api/Lesson/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("vis/{id}")]
    public async Task<IActionResult> PutLesson(int id, Lesson lesson)
    {
      if (id != lesson.Id)
      {
        return BadRequest();
      }

      await _lessonRepository.UpdateAsync(lesson);

      return NoContent();
    }

    // POST: api/Lesson
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Discipline>> PostLesson(Lesson lesson)
    {
      //var discipline = await _disciplineRepository.GetByIdAsync(disciplineId);
      //var day = await _dayRepository.GetByDateAsync(date);
      //await _disciplineRepository.UpdateAsync(discipline);
      //return CreatedAtAction("GetDiscipline", new { id = disciplineId }, discipline);

      return NoContent();
    }

    // DELETE: api/Lesson/5
    [HttpDelete("vis/{id}")]
    public async Task<IActionResult> DeleteLesson(int id)
    {

      var lesson = await _lessonRepository.GetByIdAsync(id);
      if (lesson == null)
      {
        return NotFound();
      }


      await _lessonRepository.DeleteAsync(id);

      return NoContent();
    }

    private bool LessonExist(int id)
    {
      return _context.Lessons.Any(e => e.Id == id);
    }
  }
}
