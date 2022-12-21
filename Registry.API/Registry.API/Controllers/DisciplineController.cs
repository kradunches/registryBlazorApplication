using Microsoft.AspNetCore.Mvc;
using Registry.Domain.Model;
using Registry.Infrastructure.Data;

namespace Registry.API.Controllers
{
  [Route("api/discipline/[controller]")]
  [ApiController]
  public class DisciplineController : Controller
  {
    private readonly Context _context;
    private readonly DisciplineRepository _disciplineRepository;
    private LessonRepository _lessonRepository;
    public DisciplineController(Context context)
    {
      _context = context;
      _disciplineRepository = new DisciplineRepository(_context);
    }
    // GET: api/Discipline
    [HttpGet()]
    public async Task<ActionResult<IEnumerable<Discipline>>> GetDisciplines()
    {
      return await _disciplineRepository.GetAllAsync();
    }

    // GET: api/Discipline/5
    [HttpGet("stud/{id}")]
    public async Task<ActionResult<Discipline>> GetDiscipline(int id)
    {

      var person = await _disciplineRepository.GetByIdAsync(id);
      if (person == null)
      {
        return NotFound();
      }
      return person;
    }

    // GET: api/Discipline/5
    [HttpGet("gr/{id}")]
    public async Task<ActionResult<Discipline>> GetDisciplinesFromGroup(string groupName)
    {

      var person = await _disciplineRepository.GetByGroupAsync(groupName);
      if (person == null)
      {
        return NotFound();
      }
      return person;
    }

    // PUT: api/Discipline/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("st/{id}")]
    public async Task<IActionResult> PutDiscipline(int id, Discipline discipline)
    {
      Console.WriteLine("JOJO_REFERENCE_TWO");
      if (id != discipline.Id)
      {
        return BadRequest();
      }

      await _disciplineRepository.UpdateAsync(discipline);

      return NoContent();
    }

    // POST: api/Discipline
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Discipline>> PostDiscipline(Discipline discipline)
    {
      await _disciplineRepository.AddAsync(discipline);
      return CreatedAtAction("GetDiscipline", new { id = discipline.Id }, discipline);

      return NoContent();
    }

    // DELETE: api/Discipline/5
    [HttpDelete("per/{id}")]
    public async Task<IActionResult> DeletePerson(int id)
    {

      var person = await _disciplineRepository.GetByIdAsync(id);
      if (person == null)
      {
        return NotFound();
      }


      await _disciplineRepository.DeleteAsync(id);

      return NoContent();
    }

    private bool DisciplineExist(int id)
    {
      return _context.Disciplines.Any(e => e.Id == id);
    }
  }
}
