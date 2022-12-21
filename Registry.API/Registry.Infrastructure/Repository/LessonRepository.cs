using Registry.Domain.Model;
using Registry.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Registry.Infrastructure.Repository
{
  public class LessonRepository
  {
    private readonly Context _context;
    public Context UnitOfWork
    {
      get
      {
        return _context;
      }
    }
    public LessonRepository(Context context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<List<Lesson>> GetAllAsync()
    {
      return await _context.Lessons.OrderBy(p => p.Id).ToListAsync();
    }
    public async Task<Lesson> GetByIdAsync(int id)
    {
      return await _context.Lessons
          .Where(p => p.Id == id)
      .FirstOrDefaultAsync();
    }

    public async Task<Lesson> GetByStudentNameAsync(string studentName)
    {
      return await _context.Lessons
          .Where(p => p.Discipline.Name == studentName)
      .FirstOrDefaultAsync();
    }

    public async Task<Lesson> GetBySubjectNameAsync(string name)
    {
      return await _context.Lessons
          .Where(p => p.Topic == name)
      .FirstOrDefaultAsync();
    }

    public async Task<Lesson> GetByTopicAsync(string topic)
    {
      return await _context.Lessons
          .Where(p => p.Topic == topic)
          .FirstOrDefaultAsync();
    }

    public async Task AddAsync(Lesson lesson)
    {
      _context.Lessons.Add(lesson);
      await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
      Lesson person = await _context.Lessons.FindAsync(id);
      _context.Remove(person);
      await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Lesson lesson)
    {
      var existLesson = GetByIdAsync(lesson.Id).Result;
      if (existLesson != null)
      {
        _context.Entry(existLesson).CurrentValues.SetValues(lesson);

      }

      await _context.SaveChangesAsync();
    }

    public void ChangeTrackerClear()
    {
      _context.ChangeTracker.Clear();
    }
  }
}
