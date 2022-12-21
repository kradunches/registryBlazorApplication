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
      //���������� ���� ������� ���� ������, �� � ������
      //��� �������� ���� ������ ������ ���������� �� ���� ������ �������
      var contextOptions = new DbContextOptionsBuilder<Context>()
          .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test1")
          .Options;

      _context = new Context(contextOptions);


      _context.Database.EnsureDeleted();
      _context.Database.EnsureCreated();

      //�������� �������������� ���� �� ������. ���������� ��� ������������� ���������� � ������ ����� ���
      var person1 = new Discipline
      {
        Name = "����������������",
        Time = 185,

      };

      _context.Disciplines.Add(person1);
      _context.SaveChanges();
      //��������� ������������ (��������� ����� � ��)
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