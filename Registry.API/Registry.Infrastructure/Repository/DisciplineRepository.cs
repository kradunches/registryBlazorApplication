using Microsoft.EntityFrameworkCore;
using Registry.Domain.Model;
using Registry.Infrastructure.Data;

namespace Registry.Infrastructure.Repository
{
  public class DisciplineRepository
  {
    private readonly Context _context;
    public Context UnitOfWork
    {
      get
      {
        return _context;
      }
    }
    public DisciplineRepository(Context context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<List<Discipline>> GetAllAsync()
    {
      return await _context.Disciplines.OrderBy(p => p.Name).ToListAsync();
    }
    public async Task<Discipline> GetByIdAsync(int id)
    {
      return await _context.Disciplines
          .Where(p => p.Id == id)
          .FirstOrDefaultAsync();
    }

    public async Task<Discipline> GetByNameAsync(string name)
    {
      return await _context.Disciplines
          .Where(p => p.Name == name)
          .FirstOrDefaultAsync();
    }


    public async Task AddAsync(Discipline person)
    {
      _context.Disciplines.Add(person);
      await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Discipline discipline)
    {
      var existDiscipline = GetByIdAsync(discipline.Id).Result;
      if (existDiscipline != null)
      {
        _context.Entry(existDiscipline).CurrentValues.SetValues(discipline);
        foreach (var lesson in discipline.Lessons)
        {
          var existLesson = existDiscipline.Lessons.FirstOrDefault(p => p.Id == lesson.Id);
          if (existLesson == null)
          {
            existDiscipline.Lessons.Add(lesson);
          }
          else
          {
            _context.Entry(existLesson).CurrentValues.SetValues(lesson);
          }
        }
        foreach (var lesson in existDiscipline.Lessons)
        {
          if (!discipline.Lessons.Any(pn => pn.Id == lesson.Id))
          {
            _context.Remove(lesson);
          }
        }
      }

      await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
      Discipline person = await _context.Disciplines.FindAsync(id);
      _context.Remove(person);
      await _context.SaveChangesAsync();
    }

    public void ChangeTrackerClear()
    {
      _context.ChangeTracker.Clear();
    }
  }
    
}
