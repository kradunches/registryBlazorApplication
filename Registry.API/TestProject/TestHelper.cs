using Microsoft.EntityFrameworkCore;
using Registry.Domain.Model;
using Registry.Infrastructure.Data;
using Registry.Infrastructure.Repository;

namespace TestProject
{
  public class TestHelper
  {
    private readonly Context _context;
    public TestHelper()
    {
      //Используем базу обычную базу данных, не в памяти
      //Имя тестовой базы данных должно отличатсья от базы данных проекта
      var contextOptions = new DbContextOptionsBuilder<Context>()
          .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test1")
          .Options;

      _context = new Context(contextOptions);


      _context.Database.EnsureDeleted();
      _context.Database.EnsureCreated();

      //Значение идентификатора явно не задаем. Используем для идентификации уникальное в рамках теста имя
      var person1 = new Discipline
      {
        Name = "Программирование",
        Time = 185,

      };

      _context.Disciplines.Add(person1);
      _context.SaveChanges();
      //Запрещаем отслеживание (разрываем связи с БД)
      _context.ChangeTracker.Clear();
    }

    public DisciplineRepository DisciplineRepository
    {
      get
      {
        return new DisciplineRepository(_context);
      }
    }
  }
}