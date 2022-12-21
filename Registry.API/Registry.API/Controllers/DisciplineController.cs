using Microsoft.AspNetCore.Mvc;
using Registry.Domain.Model;
using Registry.Infrastructure.Data;
using Registry.Infrastructure.Repository;

namespace Registry.API.Controllers
{
    /////
    ///
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplinesController : ControllerBase
    {
        private readonly Context _context;
        private DisciplineRepository _disciplineRepository;
        public DisciplinesController(Context context)
        {
            _context = context;
            _disciplineRepository = new DisciplineRepository(_context);
        }

        // GET: api/Disciplines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Discipline>?>> GetDisciplines()
        {
            return await _disciplineRepository.GetAllAsync();
        }

        // GET: api/Disciplines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Discipline>> GetDiscipline(int id)
        {

            var person = await _disciplineRepository.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return person;
        }

        // PUT: api/Disciplines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiscipline(int id, Discipline discipline)
        {
            if (id != discipline.Id)
            {
                return BadRequest();
            }

            await _disciplineRepository.UpdateAsync(discipline);

            return NoContent();
        }

        // POST: api/Disciplines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Discipline>> PostDiscipline(Discipline discipline)
        {
            await _disciplineRepository.AddAsync(discipline);
            return CreatedAtAction("GetDiscipline", new { id = discipline.Id }, discipline);
        }

        // DELETE: api/Disciplines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            Console.WriteLine("deleted");
            var person = await _disciplineRepository.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }


            await _disciplineRepository.DeleteAsync(id);

            return NoContent();
        }
       

        private bool DisciplineExists(int id)
        {
            return _context.Disciplines.Any(e => e.Id == id);
        }
    }
}
